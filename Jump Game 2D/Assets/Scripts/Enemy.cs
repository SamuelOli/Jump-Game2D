using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SCRIPT DO INIMIGO DO JOGO
public class Enemy : MonoBehaviour
{
    [Header("Components")]
    public Animator anim; //Referência ao Animator do inimigo
    public Rigidbody2D rig; //Referência ao rigidbody2D do Inimigo
    public BoxCollider2D box; //referência ao boxCollider2D do inimigo
    public Transform headPoint; //objeto acima da cabeça para detectar colisão ao pular em cima
    public Transform collPoint; //objeto a frente para detectar quando o player vem de frente

    [Header("Stats")]
    public float speed; //velocidade do inimigo
    public int health; //vida do inimigo

    [Header("Hit Settings")]
    public float headArea; //tamanho da área de colisão na cabeça
    public LayerMask playerLayer; //layer do player
    public float throwPlayerForce; //força no qual o impacto na cabeça arremessa o player para cima

    private bool isRight; //variável de controle para saber se o player está indo para direita ou esquerda
    private Vector2 direction; //variável de controle para saber se está pressionando o teclado ou não

    //Update é chamado a cada frame
    void Update()
    {
        //se estiver indo para a direita, rotaciona o player para direita
        if(isRight)
        {
            direction = Vector2.right;
            transform.eulerAngles = new Vector2(0, 180);
        }
        else //se for esquerda
        {
            direction = -Vector2.right;
            transform.eulerAngles = new Vector2(0, 0);
        }

        Destroy(); //método para destruir o inimigo
        
    }

    //este método é semelhante ao update, porém, deve ser usado somente para físicas
    void FixedUpdate()
    {        
        rig.MovePosition(rig.position + direction * speed * Time.deltaTime); //movendo o personagem para determinada direção
                
        Hit(); //detecta quando o player encosta
    }

    void Hit()
    {
        Collider2D hit = Physics2D.OverlapCircle(headPoint.position, headArea, playerLayer); //armazena colisão se na cabeça
        Collider2D hitPlayer = Physics2D.OverlapCircle(collPoint.position, headArea, playerLayer); //armazena colisão se na frente

        //se o player estiver colidindo na cabeça, lê-se este código
        if (hit != null)
        {
            //checa se o player não está invulneravel para poder aplicar dano
            if(hit.GetComponent<Player>().vulneravel == false)
            {
                health--;
                anim.SetTrigger("hit");
                hit.GetComponent<Rigidbody2D>().AddForce(Vector2.up * throwPlayerForce, ForceMode2D.Impulse);
            }
        
        }

        //é chamado quando escosta no player de frente
        if(hitPlayer != null)
        {
            //chama método que criamos no player para gerar dano
            hitPlayer.GetComponent<Player>().GerarDano();
        }
    }

    
    void Destroy()
    {
        //se a vida for menor do que zero, destroi o inimigo
        if(health <= 0)
        {
            anim.SetTrigger("die");
            speed = 0f;
            Destroy(gameObject, 1f);
        }
    }

    //método chamado ao colidir com alguma coisa
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checa se está colidindo com algum obstáculo
        if(collision.gameObject.layer == 9)
        {
            isRight = !isRight;
        }
    }

    //desenha um gizmo (ícone) quando seleciona o inimigo para mostrar as áreas de colisões
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(headPoint.position, headArea);
        Gizmos.DrawSphere(collPoint.position, headArea);
    }

}
