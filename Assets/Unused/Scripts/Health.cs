using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public GameObject player;
    public PlayerHealth healthPlayer;
    public Text textHealth;
    private string health;
    // Use this for initialization
    void Start()
    {

    }
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthPlayer = player.gameObject.GetComponent<PlayerHealth>();

    }

    // Update is called once per frame

    public GameObject HealthScore; // встаити сюде текстове меню яке буде виводити здорове

    void Update()
    {




        health = "";

        textHealth = this.GetComponent<Text>();
        //Health.text ="Health="+DataHolder.PlayerHealth.ToString();

        for (int i = 0; i < healthPlayer.currentHealth; i++)
        { health = health + "I"; }
        textHealth.text = "Health =" + health;


    }
}
