using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryClick : MonoBehaviour {
	public Inventory inventory;
	public int InventoryNumber;



	public void Clicked()
	{
		

		inventory.RemoveItem (InventoryNumber);


	}

	void onGUI(){}

}
