using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject player;
	public GameObject contactedActivItem;

	private void OnTriggerEnter(Collider other)
	{

		if (other.tag == "Enemy") {
			int thisWeaponDamgae = this.GetComponent<WeaponStats> ().weaponDamage;
			EnemyHealth healthEnemy =other.gameObject.GetComponent<EnemyHealth>();

			if(healthEnemy != null){healthEnemy.DamageTaken (thisWeaponDamgae);}
			player=GameObject.FindGameObjectWithTag ("Player");

				if (player.GetComponent<PlayerWeapons> ().activItem != null)
				{	
				 
					contactedActivItem=player.GetComponent<PlayerWeapons> ().activItem;
				contactedActivItem.GetComponent<ActivDamageMetter> ().damageNow+=thisWeaponDamgae;
				}


			Destroy (gameObject);


		}

		if (other.tag == "Room") { 
			Destroy (gameObject);
		}



	}



	}

