using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float damage = 1;

    GameObject coll = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coll != null && coll.transform.tag == "Player")
        {
            
            coll.GetComponentInParent<Player>().GerarDano(damage);   
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            //Debug.Log("Player detect");
            coll = collision.gameObject;
            //collision.gameObject.GetComponentInParent<Player>().GerarDano(damage);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            //Debug.Log("Player detect");
            coll = collision.gameObject;
            //collision.gameObject.GetComponentInParent<Player>().GerarDano(damage);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            coll = null;
        }
    }

}
