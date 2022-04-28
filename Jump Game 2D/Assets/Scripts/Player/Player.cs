using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rig; //referencia o componente rigidbody2D do player
    public Animator anim; //referencia o componente Animator
    public SpriteRenderer spriteRenderer; //referencia o componente SpriteRenderer

    float time;

    public float vidaInicial;
    public float vida; //armazena quantidade de vida
    public bool isImmortal; //verifica se o Player pode morrer. Para que os testes sejam realizados

    public float velocidade; //armazena velocidade do player
    public float forcaPulo; //armazena força do pulo

    //RaycastHit2D hit = new RaycastHit2D(); //variavel que detecta colisão com o chão
    public bool estaPulando; //variável de controle para saber se player está pulando
    public bool vulneravel; //variável de controle para saber se player está invulnerável

    //private GameController gc; //variável de controle para armazenar o objeto na cena com o script GameController
    float direcao; //variável de controle para saber se o player está indo para direita ou esquerda

    public GameController gc;

    public bool finish = false;
    public bool exitFinish = false;

    // Start é chamado uma vez ao inicializar o playmode
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        vida = vidaInicial;
    }

    // Update é chamado a cada frame
    void Update()
    {
        //retorna uma direcao no eixo x com valor entre -1 (esquerda) e 1 (direita)
        direcao = Input.GetAxis("Horizontal");

        //checa se está indo para direita
        if(direcao > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);

        }

        //checa se está indo para esquerda
        if(direcao < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
            
        }

        //condição dupla: se está movendo e não está pulando
        if(direcao != 0 && !estaPulando)
        {
            anim.SetInteger("transition", 1);
        }

        //condição dupla: se está parado e não está pulando
        if(direcao == 0 && !estaPulando)
        {
            anim.SetInteger("transition", 0);
        }

        Jump(); //método que faz o player pular

        //Conta o tempo;
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.G))
        {
            gc.GameOver();
        }
        
    }

    //funcionalidade parecida com o update, porém, usado para físicas
    private void FixedUpdate()
    {
        int i = 1;
        if (rig.velocity.y > 0)
        {
            i = 0;
        }
        if (!finish) //Movimentação do Player em game
        {
            rig.velocity = new Vector2(direcao * velocidade, rig.velocity.y); //move o player
        }
        else if(exitFinish) 
        {
            rig.velocity = new Vector2(direcao * velocidade / 15, rig.velocity.y / 2 * i); //Movimentação do Player depois de ter saido da linha de chegada
        }
        else
        {
            rig.velocity = new Vector2(direcao * velocidade / 7, rig.velocity.y / 1.2f * i); //Movimentação do Player quando estiver na linha de chegada
        }
    }

    void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !estaPulando && !finish)
        {
            rig.AddForce(Vector2.up * forcaPulo, ForceMode2D.Force); //adiciona um impulso para cima
            anim.SetInteger("transition", 2); //altera a animação para a de pulo
            estaPulando = true; //altera a variável de controle estaPulando para verdadeiro
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !estaPulando && !finish)
        {
            rig.AddForce(Vector2.up * forcaPulo, ForceMode2D.Force); //adiciona um impulso para cima
            anim.SetInteger("transition", 2); //altera a animação para a de pulo
            estaPulando = true; //altera a variável de controle estaPulando para verdadeiro
        }
    }

    //método que dá dano no player
    public void GerarDano()
    {
        GerarDano(1);
    }

    public void GerarDano(float damage)
    {
        //Debug.Log("Take Damage " + damage);
        //checa se não está invulnerável
        if (vulneravel)
        {
            vida -= damage; //tira vida do player
            vulneravel = false; //ativa a invulnerabilidade
            gc.PerderVida(vida, vidaInicial); //chama o método do GameController que atualizar a barra de vida
            if (!isImmortal && vida <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(Respawn()); //corrotina que ativa e desativa o sprite do personagem
            }
        }
    }

    float tempoPiscar = .3f; //tempo de piscar
    IEnumerator Respawn()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(tempoPiscar);        
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(tempoPiscar);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(tempoPiscar);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(tempoPiscar);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(tempoPiscar);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(tempoPiscar);
        vulneravel = true;
    }

    
    public void Die()  //Morte do Player
    {
        gc.GameOver();
        GameObject.Destroy(gameObject);
    }

    public void CheckPoint() //Player completou a fase
    {
        //CountPoints();
        //anim.SetInteger("transition", 0);
        rig.velocity = new Vector2(direcao * 3, rig.velocity.y); //move o player
        StartCoroutine(NextScene());
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(2.2f);
        //SceneManager.LoadScene(1);
        gc.Win();
    }

    /*
    public int CountPoints() //Calcula os pontos do Player
    {
        float total = 0;
        total += totalCoins;
        total *= 5 * vida / vidaInicial;
        total -= time* .1f;

        Debug.Log("Total: " + total + "\nTempo: " + time);

        return (int)total;
    }*/

    //Verifica constantemente no que o objeto está encostando

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {
            finish = true;
            collision.gameObject.GetComponent<Animator>().SetTrigger("check");
            CheckPoint();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {
            exitFinish = true;
        }
    }


}
