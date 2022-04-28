using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 8)
        {
            player.estaPulando = false;
        }
        if (collision.gameObject.layer == 11)
        {
            player.estaPulando = false;
            player.GerarDano();
        }
        if (collision.gameObject.layer == 12)
        {
            player.CheckPoint();
        }
    }

    //Verifica se o objeto deixou de encostar em algum outro
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 8)
        {
            player.estaPulando = true;
            player.anim.SetInteger("transition", 2);
        }
        if (collision.gameObject.layer == 11)
        {
            player.estaPulando = true;
            player.anim.SetInteger("transition", 2);
        }
    }
    
}
