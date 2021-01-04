using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    // The amount of health the enemy starts the game with.
    public float startingHealth = 3;
    // The current health the enemy has.
    public float currentHealth;
    private float stunTime = 0.2f;
    private float invunrabilityTime = 1.2f;
    private bool isStunActive = false;
    private bool isInvunrabilityActive = false;
    private GameObject cameraToShake;

    void Start()
    {

    }
    void Awake()
    {
        //	currentHealth = startingHealth;
        cameraToShake = GameObject.FindGameObjectWithTag("MainCamera");
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentHealth > startingHealth)
            currentHealth = startingHealth;

        if (isInvunrabilityActive == true)
            invunrabilityTime -= Time.deltaTime;

        if (invunrabilityTime <= 0)
        {
            isInvunrabilityActive = false;
            invunrabilityTime = 1.2f;
            Debug.Log("invunrability passed");

        }

        if (isStunActive == true)
        {
            //			Debug.Log ("Stun Started+ stun time= " + stunTime);
            stunTime -= Time.deltaTime;
            var shikeMaster = cameraToShake.GetComponent<CamerFOlow>();
            shikeMaster.cameraShake();
        }

        if (stunTime <= 0)
        {

            isStunActive = false;
            stunTime = 0.2f;
        }

    }
    public void DamageTaken()
    {

        if (isInvunrabilityActive == false)
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                //	SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
                SceneManager.LoadScene(0);
            }
            isStunActive = true;
            isInvunrabilityActive = true;
            Debug.Log("Invunrability active");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            DamageTaken();

            /*DataHolder.PlayerHealth--;

			if (DataHolder.PlayerHealth < 1) 
			{
				//restart the game
				DataHolder.Score = 0;SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
			}*/
        }
    }
}
