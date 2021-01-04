using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTriger : MonoBehaviour {
	public GameObject RoomPrefab;
	public bool DoorActive=false;
	 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter (Collider other) 
	{
		if (other.tag == "Player") {
			if (DoorActive == false) {
				RoomDoorsSpawner other1 = RoomPrefab.GetComponent<RoomDoorsSpawner> ();
				DoorActive = true;
				other1.CloseDors ();
			}
		}
	}

}
