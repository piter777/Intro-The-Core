using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEnemyRoket : MonoBehaviour {
	public float speed;
	public GameObject plyerPosition;
	// Use this for initialization
	void Start () {
		plyerPosition=GameObject.FindWithTag("Player");
		Vector3 playerTarget = plyerPosition.transform.position;
		Vector3 direction = ((Vector2)plyerPosition.transform.position - (Vector2) transform.position).normalized;
		GetComponent<Rigidbody2D>().velocity = direction * speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
