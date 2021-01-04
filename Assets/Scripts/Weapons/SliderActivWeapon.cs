using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderActivWeapon : MonoBehaviour {

	public Slider slider;
	public GameObject player;
	public GameObject contactedActivItem;
	public GameObject fillarena;
	// Use this for initialization
	void Start () 
	{
		player=GameObject.FindGameObjectWithTag ("Player");
		//uiSlider.GetComponent<Slider>= slider;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player.GetComponent<PlayerWeapons>().activItem != null)
		{	
			fillarena.SetActive (true);
			contactedActivItem=player.GetComponent<PlayerWeapons> ().activItem;
		//	contactedActivItem.GetComponent<ActivDamageMetter> ().damageNow;
		//	contactedActivItem.GetComponent<ActivDamageMetter> ().damageToActivate;
			if (contactedActivItem.GetComponent<ActivDamageMetter> ().damageNow != 0) {
				float debug;
				debug = (float)contactedActivItem.GetComponent<ActivDamageMetter> ().damageNow / contactedActivItem.GetComponent<ActivDamageMetter> ().damageToActivate;
				slider.value = debug;
					
			} else 
			{	
				fillarena.SetActive (false);
				slider.value = 0;
			}
		}
		else fillarena.SetActive (false);

	}
}
