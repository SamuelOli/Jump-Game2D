using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameController gc; //referencia o objeto do Player
    

    void Start()
    {
        gc = FindObjectOfType<GameController>(); //encontra um objeto na cena que contenha o script Player e armazena na variável player
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //chamar o método que acrescenta moeda
            //gc.addFruit(amount);
            Destroy(gameObject);
        }
    }
}
