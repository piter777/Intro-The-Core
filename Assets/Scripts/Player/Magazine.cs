using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Magazine : MonoBehaviour {
	
	public GameObject player;
	public GameObject curentWeponToShow;
	
	
	// Use this for initialization
	void Start () {
		
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		curentWeponToShow = player.GetComponent<PlayerWeapons> ().curentWeapon;
	//	curentWeponToShow = player.GetComponent<PControler> ().activItem;

		var maxBullets = curentWeponToShow.GetComponent<WeaponStats>().weaponMagazine;
		var currentBullets = curentWeponToShow.GetComponent<WeaponStats> ().weaponBulletsInMagazine;
		GetComponent<Text> ().text = "ammo "+ currentBullets + "/" + maxBullets;
	}
}
