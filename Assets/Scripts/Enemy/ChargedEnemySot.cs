using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedEnemySot : MonoBehaviour {
	
	public GameObject playerPosition; 
	public Transform ThisShipPosition;
	public float fireRateRoketEnemy;
	private float nextFire;
	private float BurstFire;
	public GameObject RoketEnemy;
	public int Shootcount;
	private int CurentSutCount=0;

	void Start () 
	{
		playerPosition=GameObject.FindWithTag("Player");
		
	}

	void Shot()
	{
		var bullet = (GameObject)Instantiate(RoketEnemy, transform.position , transform.rotation* Quaternion.Euler(0, Random.Range (-10,10), 0));
		bullet.transform.LookAt (playerPosition.transform.position);
		bullet.transform.SetParent (GameObject.FindGameObjectWithTag ("Dynamic").transform);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 15;
		Destroy(bullet, 3.0f);
	}

	void Update () 
	{
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRateRoketEnemy;

			CurentSutCount = 0;
		}
		
		if ((Time.time > BurstFire) && (CurentSutCount < Shootcount)) 
		{
			BurstFire = Time.time + 0.05f; 
			Shot();

			CurentSutCount++;

		}
		
	}
}
