using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoverTransate : MonoBehaviour {
	public float forseMult = 20;
	// transform translate
	void Update(){


		var moveh = Input.GetAxis ("Horizontal") * Time.deltaTime  ;
		var movev = Input.GetAxis ("Vertical") * Time.deltaTime ;
		Vector3 movement = new Vector3 (moveh,0f,movev);
	//	transform.Rotate (0,x,0);
		transform.Translate (movement * forseMult);


	}


}
