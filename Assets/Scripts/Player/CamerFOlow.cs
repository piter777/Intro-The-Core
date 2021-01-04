using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFOlow : MonoBehaviour {
	public Transform target;            // The transform that that camera will be following.
	public int cameraRangeMover=20;
	public int cameraRange = 25;

	void Awake()
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	
	// Update is called once per frame
	void LateUpdate() {
		// Create a position the camera is aiming for based on the offset from the target.
		var mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		var screenX = (mousePosition.x - Screen.width / 2) / cameraRangeMover;
		var screenY = (mousePosition.y - Screen.height / 2) / cameraRangeMover;
		var middleCoordinates = new Vector3(target.position.x + (screenX), cameraRange, target.position.z + (screenY));

		transform.position = middleCoordinates;
	}
	public void cameraShake()
	{
		transform.position=new Vector3(transform.position.x,transform.position.y+2,transform.position.z);
	}
}
