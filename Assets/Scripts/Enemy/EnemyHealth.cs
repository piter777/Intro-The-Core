using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour {
	
	// The amount of health the enemy starts the game with.
	public float startingHealth = 100;     
	// The current health the enemy has.
	public float currentHealth;                   

	void Awake()
	{
		currentHealth = startingHealth;

	}


	public virtual void DamageTaken(float WeaponDamage)
    {


		currentHealth -=	WeaponDamage;
		if (currentHealth <= 0)
			
		{ 

			Destroy (gameObject);
		//	DataHolder.Score++;
		}
	
	
	}


}
