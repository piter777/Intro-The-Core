using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapFolow : MonoBehaviour {
	public Transform target;
	public float smoothing=5f;
	Vector3 offset;
	// Use this for initialization
	void Start () 
	{
		offset = transform.position - target.position;
	}
	void Awake()
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	void FixedUpdate ()
	{
		// Create a postion the camera is aiming for based on the offset from the target.
		Vector3 targetCamPos = target.position + offset;



		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
