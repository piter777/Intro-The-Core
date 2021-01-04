using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRendere : MonoBehaviour {

	private	GameObject player;
	public GameObject laserLight;
	private LineRenderer lr;
	// Use this for initialization
	void Start () {
		lr = GetComponent<LineRenderer> ();
		player=  GameObject.FindGameObjectWithTag ("Player");
		laserLight.transform.SetParent (this.transform.parent);



	}
	private float nextFire;
	// Update is called once per frame
	void Update ()
	{


		lr.SetPosition (0, transform.position);
		RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if ((hit.collider) && (hit.collider.tag != "Trigger"))
            {
                lr.SetPosition(1, hit.point);
                if (((hit.collider.tag == "Enemy") || (hit.collider.tag == "Projective")) && (Time.time > nextFire))
                {
                    //Relaod Time
                    nextFire = Time.time + (this.GetComponent<WeaponStats>().weaponReloadTime - (this.GetComponent<WeaponStats>().weaponReloadTime * player.GetComponent<PlayerWeapons>().atackSPeedBuff / 100));
                    EnemyHealth healthEnemy = hit.collider.gameObject.GetComponent<EnemyHealth>();
                    if (healthEnemy != null)
                    {
                        float DamageBuffAdd = player.GetComponent<PlayerWeapons>().damageBuff;
                        float DamageTOAdd = this.GetComponent<WeaponStats>().weaponDamage + (this.GetComponent<WeaponStats>().weaponDamage * DamageBuffAdd / 100);
                        healthEnemy.DamageTaken(DamageTOAdd);
                    }
                }
            }
            else 
			{
				//laserLight.transform.position = hit.transform.position;
				lr.SetPosition (1, transform.forward * 2000);
			}









		}
		laserLight.SetActive (true);
		laserLight.transform.position = new Vector3 (lr.GetPosition (1).x, 1f, lr.GetPosition (1).z);


		//Debug.Log (laserLight.transform.localPosition+" "+lr.GetPosition (1)+""+laserLight.transform.TransformPoint (lr.GetPosition (1)));
	}
}
