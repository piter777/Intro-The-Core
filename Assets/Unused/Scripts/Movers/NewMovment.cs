using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnitySampleAssets.CrossPlatformInput;
public class NewMovment : MonoBehaviour {


	public float speed = 6f;            // The speed that the player will move at.


	Vector3 movement;                   // The vector to store the direction of the player's movement.
	Animator anim;                      // Reference to the animator component.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.


	void Awake ()
	{
		playerRigidbody = GetComponent <Rigidbody> ();
	}

	void Start () {
		
	}
	
	// Update is called once per frame


	void FixedUpdate ()
	{
		// Store the input axes.
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		// Move the player around the scene.

		Move (h, v);
	//	Rotating ();
		// Turn the player to face the mouse cursor.
	//	Turning ();

		// Animate the player.
	//	Animating (h, v);
	}
	void Move (float h, float v)
	{
		// Set the movement vector based on the axis input.
		movement.Set (h, 0f, v);

		// Normalise the movement vector and make it proportional to the speed per second.

		movement = movement.normalized * speed * Time.deltaTime;

		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition (transform.position +(movement));

		if ((h == 0) && (v == 0)) {
			playerRigidbody.isKinematic = true;
		} else
			playerRigidbody.isKinematic = false;
	//	transform.Translate ((transform.forward*Input.GetAxisRaw("Vertical") + transform.right*Input.GetAxisRaw("Horizontal")).normalized *speed*Time.deltaTime); 
	}


	//float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
	//	return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;}


	void Turning ()
	{	
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		Vector3 turnDir = new Vector3(Input.GetAxisRaw("Mouse X") , 0f , Input.GetAxisRaw("Mouse Y"));

		turnDir = camRay.direction*-1f;

		if (turnDir != Vector3.zero)
		{
			Vector3 playerToMouse = (transform.position + turnDir) - transform.position;	
			playerToMouse.y = 0f;		
			Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);		
			playerRigidbody.MoveRotation(newRotatation);
		}

	}







	void Animating (float h, float v)
	{

	}




}
