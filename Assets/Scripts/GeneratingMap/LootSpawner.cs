using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour {



	public GameObject[] PikableSPawned;
	private Transform boardHolder; 
	// Use this for initialization
	void Start () {

		//GameObject toInstantiate = PikableSPawned [Random.Range(0,PikableSPawned.Length)];

	//	Instantiate (toInstantiate,new Vector3  (this.transform.position.x ,this.transform.position.y+1 , this.transform.position.z), Quaternion.identity);
		GameObject toInstantiate =Instantiate ( PikableSPawned [Random.Range(0,PikableSPawned.Length)],new Vector3  (this.transform.position.x ,this.transform.position.y+1 , this.transform.position.z), Quaternion.identity);
		boardHolder = GameObject.FindGameObjectWithTag ("Dynamic").transform;
		toInstantiate.transform.SetParent (boardHolder);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
