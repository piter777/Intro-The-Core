using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSlider : MonoBehaviour
{

    public Slider slider;
    public GameObject boss;
    public GameObject health;
    public GameObject fillarena;
    // Start is called before the first frame update
    void Start()
    {
       // health = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        boss = GameObject.FindGameObjectWithTag("Enemy");
        if ((boss != null))
        {
            fillarena.SetActive(true);
            float healthNow = boss.GetComponent<EnemyHealth>().currentHealth;
            //	contactedActivItem.GetComponent<ActivDamageMetter> ().damageNow;
            //	contactedActivItem.GetComponent<ActivDamageMetter> ().damageToActivate;
            if (boss.GetComponent<EnemyHealth>().currentHealth > 0) {
                var debug = (float)boss.GetComponent<EnemyHealth>().currentHealth / boss.GetComponent<EnemyHealth>().startingHealth;
                slider.value = debug;
            }
            else
            {
                fillarena.SetActive(false);
                slider.value = 0;
            }
        }
        else
        {
            fillarena.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
