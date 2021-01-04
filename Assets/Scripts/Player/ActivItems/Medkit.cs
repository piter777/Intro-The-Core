using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour {

	// Use this for initialization

	void Awake()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		player.GetComponent<PlayerHealth> ().currentHealth+=2;
		Destroy (gameObject);


	}
	
	// Update is called once per frame

}
