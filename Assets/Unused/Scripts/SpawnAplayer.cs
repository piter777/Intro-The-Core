using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAplayer : MonoBehaviour {
	public GameObject playerLoaded;
	// Use this for initialization
	void Start () {
		GameObject player= GameObject.FindGameObjectWithTag ("Player");
		playerLoaded = player;
		player.transform.position = this.transform.position;
		player.transform.parent = this.transform;
		player.transform.parent = null;
	//	player.SetActive (false);
		 
	//	GameObject	PlayerSpawn = (GameObject)Instantiate (DataHolder.Player, this.transform.position, this.transform.rotation * Quaternion.Euler (0, 0, 0));

	//	PlayerSpawn.SetActive (true);



	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
