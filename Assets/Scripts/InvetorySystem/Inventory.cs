using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour {
    public const int numItemSlots = 2;
    public Image[] itemImages =new Image[numItemSlots];
	public GameObject[] items = new GameObject[numItemSlots];
	private Sprite testSprite;






	public WeaponStatsInventory[] WSI  =new WeaponStatsInventory[numItemSlots];

	public class WeaponStatsInventory {
		public int weaponType { get; set;}
		public int weaponDamage{ get; set;}
		public float weaponReloadTime { get; set;}
}






	// создаем новую шмотку в инвантаре на клике
	public void AddItem(GameObject itemToadd,Sprite spriteToLoad)
    {

		for (int i = 0; i < items.Length; i++) {

			if (items [i] == null) {
				items [i] = itemToadd;

				itemImages [i].sprite = spriteToLoad;
				itemImages [i].enabled = true;
				WSI [i] = new WeaponStatsInventory ();
				WSI [i].weaponDamage = itemToadd.GetComponent<WeaponStats>().weaponDamage;
				WSI [i].weaponType = itemToadd.GetComponent<WeaponStats>().weaponType;
				WSI [i].weaponReloadTime = itemToadd.GetComponent<WeaponStats>().weaponReloadTime;

				return;

			}
		}
       

    }

	public void RemoveItem (int invNum)
	{
		// Go through all the item slots...

			// ... if the item slot has the item to be removed...
		//	if (items[i] == itemToRemove){}
		
				// ... set the item slot to null and set the image component to display nothing.
		items[invNum] = null;
		itemImages[invNum].sprite = null;
		itemImages[invNum].enabled = false;
				
			

	}
    



}
 

