using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//создаю движения вперед в сторону мишки от сторони спавна вистрела
public class Mover : MonoBehaviour {

	public float speed;
	void Start () {
		
		//Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	//	Vector2 direction = (mouseScreenPosition - (Vector2) transform.position).normalized;
		/*
		Transform selfpoition = GetComponent<Transform> ();
		selfpoition.position.Set (selfpoition.position.x + 0.001f,selfpoition.position.y + 0.001f,selfpoition.position.z + 0.001f);
		Vector2 selfpos = selfpoition.position;
		Vector2 direction = ((Vector2)selfpos - (Vector2) transform.position).normalized;*/
		//поулчения движения


		//Vector2 direction =  (Vector2) transform.position.normalized;


	//	 GetComponent<Rigidbody2D>().velocity = direction * speed; //само движения * направления
	//	GetComponent<Rigidbody2D>().AddForce(transform.forward * speed);
	//	GetComponent<Rigidbody2D>().velocity = transform.forward * speed;
	//	transform.up.Set(1,2,3);



	}

	void Update()
	{
		
		GetComponent<Rigidbody>().velocity = transform.forward * 6;
	}


}
