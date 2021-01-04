using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPickUP : MonoBehaviour {
	public int HpAmount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter(Collider other)
	{   
		if (other.tag == "Player") {
			other.GetComponent<PlayerHealth> ().currentHealth += 2;
		//	local.DamageBuff += DamagePassive;
			Destroy (gameObject);



		}}
}
