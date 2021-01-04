using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerRigibodyVelosity : MonoBehaviour {

	public float forseMult = 20;
	private Rigidbody rb;



	void Awake()
	{
		rb = GetComponent<Rigidbody> ();
	


	}
	// Update is called once per frame
	void Update () 
	{

		float moveh = Input.GetAxisRaw("Horizontal");
		float movev = Input.GetAxisRaw("Vertical");
		Vector3 movement = new Vector3 (moveh,0f,movev);
		rb.velocity = movement  * forseMult;

	
			
		// best way to move from now on
	}



}
