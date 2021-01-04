using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
	
	public List<GameObject> WeaponsIninventory  = new List<GameObject> ();
	public GameObject CurentWeapon;
	private int NumberOfWeapon=0;
	public PlayerWeapons playerWeaponsLink;
//	public List<GameObject> WeaponsIninventory { get; set;}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {



		if (MouseWheelForward())
		{
			if(WeaponsIninventory.Count>1)
			{
				if (NumberOfWeapon + 1 == WeaponsIninventory.Count)
				{
					NumberOfWeapon=0;
				}
				else 
				{NumberOfWeapon++;}

				CurentWeapon= WeaponsIninventory[NumberOfWeapon];
//				PControlerLink.FireWithGuns(CurentWeapon);
			}
		
		
		
		}
		
	}

	private bool MouseWheelForward() {
		return Input.GetAxis("Mouse ScrollWheel") > 0f;
	}


	public void AddItem(GameObject itemToadd)
	{
		WeaponsIninventory.Add (itemToadd);
	}
}


