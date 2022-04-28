using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    public int totalFruits; //armazena total de frutas
    public Text fruitText; //referencia o texto das frutas
    public int maxFruits; //armazena o total de frutas
    public GameObject fruits;

    public Image healthBar; //referencia barra de vida
    public int health;
    int healthValor;

    public Text txtTime;
    float time;


    public GameObject gameOver;
    public GameObject win;

    public static GameController instantiate; //Usado para acessar as variaveis da classe

    public void Start()
    {
        instantiate = this;
        health = 1;
        time = 0;
        if(totalFruits >= healthValor)
        {
            addHealth();
        }
        maxFruits = fruits.transform.childCount;
        fruitText.text = totalFruits + " / " + maxFruits;
    }

    public void Update()
    {
        time += Time.deltaTime;
        //txtTime.text = time.ToString("F2");
        txtTime.text = ConvertTime(time);
        fruitText.text = totalFruits + " / " + maxFruits;
    }

    public string ConvertTime(float t)
    {
        return TimeSpan.FromSeconds(t).Minutes.ToString("00") + ":" + TimeSpan.FromSeconds(t).Seconds.ToString("00");
    }

    //método chamado para adicionar moedas à variável totalCoins e depois atualiza no texto
    public void addFruit(int fruit)
    {
        totalFruits+= fruit;
        fruitText.text = totalFruits + " / " + maxFruits;
    }

    public void addHealth()
    {
        health++;
        totalFruits -= healthValor;
    }

    //método chamado para atualizar o visual da barra de vida de acordo com a quantidade
    public void PerderVida(float health, float maxHealth)
    {
        healthBar.fillAmount = health / maxHealth;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void Win()
    {
        win.SetActive(true);
    }

    public void Next()
    {
        int x = SceneManager.GetActiveScene().buildIndex;
        x++;
        SceneManager.LoadScene(x);
    }
}
