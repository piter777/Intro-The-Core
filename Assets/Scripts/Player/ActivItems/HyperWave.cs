using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperWave : MonoBehaviour, IUsable {

	public GameObject weaponShot;
	private GameObject player;
//	public UnityScript script;



	void Awake()
	{
		//Lag hard
		//ChirleSoot (); 
		StartCoroutine(	ChirleSoot4 () );
		//	Activate();
	}

	private	IEnumerator ChirleSoot4()
	{
		WaitForSeconds wait = new WaitForSeconds( 0.9f ) ;
		for (int i = 0; i < 4; i++) 
		{ 
			//	Debug.Log ("i="+  i%2);  //1=1 3=1 5=1
			for(float j=0;j<360;j+=10)
			{	
				float gradus = j;
				if (i % 2 == 1) { gradus += 5; }
				var bullet = (GameObject)Instantiate(weaponShot, transform.position , transform.rotation* Quaternion.Euler(0, gradus, 0));

				bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;
				bullet.transform.Rotate (0, 270, 0);
				Destroy(bullet, 4.0f);
			}

			yield return wait ;
		}
		Destroy (gameObject);
	}



	void ChirleSoot()
	{
		for (int i = 0; i < 4; i++) 
		{ 
			//	Debug.Log ("i="+  i%2);  //1=1 3=1 5=1
			for(float j=0;j<360;j+=10)
			{	float gradus = j;
				if (i % 2 == 1) { gradus += 5;
				}
				player = GameObject.FindGameObjectWithTag ("Player");
				var bullet = (GameObject)Instantiate(weaponShot, player.transform.position , player.transform.rotation* Quaternion.Euler(0, gradus, 0));

				bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;
				Destroy(bullet, 4.0f);
			}


		}
		Destroy (gameObject);
	}

	private float nextFire;

/*	void Update ()
	{
		if (Time.time > nextFire) 
		{
			
			Debug.Log ( "timers 1s");
			nextFire = Time.time + 1f;
		}

	}*/





	public	void Activate()
	{
		//ChirleSoot ();
		//StartCoroutine(	ChirleSoot () );
		//	Debug.Log("activate");
	}

}
