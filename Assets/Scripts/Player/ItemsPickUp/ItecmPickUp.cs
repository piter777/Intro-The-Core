using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItecmPickUp : MonoBehaviour
{


    public GameObject WeaponToInventory;

    public GameObject canvas;

    public Vector3 Scale;





    void Start()

    {




    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerWeapons local = other.GetComponent<PlayerWeapons>();



            // Add mite to player "inventory"
            GameObject ToAddInventory = Instantiate(WeaponToInventory, other.transform.position, Quaternion.Euler(0, 0, 0));
            ToAddInventory.SetActive(false);
            ToAddInventory.transform.SetParent(other.transform);
            ToAddInventory.transform.localScale = Scale;



            //Pop up text algorithm
            GameObject toSpawnCanvas = Instantiate(canvas, new Vector3(0, 100, 0), Quaternion.Euler(0, 0, 0));
            GameObject UIcanvas = GameObject.FindGameObjectWithTag("UI");
            toSpawnCanvas.transform.SetParent(UIcanvas.transform, false);
            var canvasText = toSpawnCanvas.transform.GetChild(1); //("PikableWeaponText");
            canvasText.GetComponent<Text>().text = WeaponToInventory.GetComponent<WeaponStats>().weaponName;
            var canvasImage = toSpawnCanvas.transform.GetChild(2);
            canvasImage.GetComponent<Image>().sprite = WeaponToInventory.GetComponent<SpriteRenderer>().sprite;
            UIcanvas.GetComponent<PopUpTextSorter>().popUpsList.Add(toSpawnCanvas);



            local.AddItem(ToAddInventory);
            Destroy(gameObject);
        }
    }




    public void OnMouseDown()
    {

        //	inventoryObj = GameObject.FindWithTag ("Inventory");
        //	if(inventoryObj=null) Debug.Log ("Knopka 3");
        //	inventory.AddItem (WeaponToInventory,sprite);
        //	Inventory.WeaponStatsInventory
        /*
		inventoryObj=GameObject.FindWithTag ("Inventory");
		inventory=inventoryObj.GetComponent<Inventory> ();
		inventory.AddItem (WeaponToInventory,sprite);

*/


    }


    void OnMouseOver()
    {
        //displayInfo = true;
        //	WeaponToInventory.GetComponent<WeaponStats>().WeaponDamage;
        //	mytext.text =WeaponToInventory.GetComponent<WeaponStats>().WeaponDamage.ToString()+"Damage"+System.Environment.NewLine+WeaponToInventory.GetComponent<WeaponStats>().WeaponReloadTime.ToString()+"Reload sec";

    }
    void OnMouseExit()
    {
        //displayInfo = false;

        //	mytext.text ="";
    }





}
