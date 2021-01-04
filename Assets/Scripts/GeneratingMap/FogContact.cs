using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogContact : MonoBehaviour {


	void OnTriggerEnter (Collider other) 
	{
		if (other.tag == "Room") {

//			Debug.Log ("Fox transform");
			Destroy (gameObject);
		}
		
	}


}
