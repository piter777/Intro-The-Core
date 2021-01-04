using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigibodyAddForse : MonoBehaviour {

	public float forseMult = 20;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		
	}
	void Awake(){rb = GetComponent<Rigidbody> ();}
	
	// Update is called once per frame
	void Update ()
	{
		//rb.AddForce (transform.forward * forseMult*Time.deltaTime);
	}
	void FixedUpdate()
	{
		float moveh = Input.GetAxisRaw("Horizontal");
		float movev = Input.GetAxisRaw("Vertical");
		Vector3 movement = new Vector3 (moveh,0f,movev);
		rb.AddForce (movement * forseMult);

	}
}
