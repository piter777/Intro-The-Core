using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning : MonoBehaviour {
	Rigidbody playerRigidbody; 
	// Use this for initialization
	void Start () {
		
	}
	void Awake(){
		playerRigidbody = GetComponent <Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		// Turn player to mouse position.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		Vector3 turnDir = new Vector3(Input.GetAxisRaw("Mouse X") , 0f , Input.GetAxisRaw("Mouse Y"));

		turnDir = camRay.direction*-1f;

		if (turnDir != Vector3.zero)
		{
			Vector3 playerToMouse = (transform.position + turnDir) - transform.position;	
			playerToMouse.y = 0f;		
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}
		
	}
}
