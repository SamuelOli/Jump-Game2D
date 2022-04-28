using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    private GameController gc; //referencia o objeto do GameController

    public int amount;
    //public TypeFruits type;

    bool collect;

    public GameObject collected;

    // Start is called before the first frame update
    void Start()
    {
        collect = true;
        gc = FindObjectOfType<GameController>(); //encontra um objeto na cena que contenha o script GameController
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collect)
        {
            collect = false;
            //chamar o método que acrescenta moeda
            gc.addFruit(amount);
            GameObject c = Instantiate(collected, transform.position, transform.rotation);
            Destroy(c, .3f);
            Destroy(gameObject);
        }
    }
}
/*
public enum TypeFruits
{
    apple,
    banana
}
*/