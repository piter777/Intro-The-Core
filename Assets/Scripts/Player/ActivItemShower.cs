using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivItemShower : MonoBehaviour {

	public GameObject player;
	public GameObject curentActivItem;
	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () 
	{
		//curentWeponToShow = player.GetComponent<PControler> ().curentWeapon;
		curentActivItem = player.GetComponent<PlayerWeapons> ().activItem;
		if (curentActivItem != null)
			this.GetComponent<Text> ().text = " " + curentActivItem.GetComponent<ActivDamageMetter>().damageNow+"/"+curentActivItem.GetComponent<ActivDamageMetter>().damageToActivate;
		else
			this.GetComponent<Text> ().text = " ";
	}
}
