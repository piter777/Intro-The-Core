using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActivItemPickUP : MonoBehaviour {
	public GameObject activWeaponToInventory;
	public GameObject canvas;

	// Use this for initialization
	void Start () {
		
	}
	
	private void OnTriggerEnter(Collider other)
	{   
		if (other.tag == "Player") 
		{
            PlayerWeapons local =other.GetComponent<PlayerWeapons>();

			GameObject	ToAddInventory = Instantiate (activWeaponToInventory, other.transform.position, Quaternion.Euler (0, 0, 0) );
			ToAddInventory.SetActive (false);
			ToAddInventory.transform.SetParent(other.transform);
		//	ToAddInventory.transform.localScale =Scale;


			local.activItem = ToAddInventory;
			//Spawn a canvas.
			GameObject	toSpawnCanvas = Instantiate (canvas, new Vector3(0,100,0), Quaternion.Euler (0, 0, 0) );
			GameObject UIcanvas = GameObject.FindGameObjectWithTag ("UI");
			toSpawnCanvas.transform.SetParent (UIcanvas.transform,false);
			var	canvasText = toSpawnCanvas.transform.GetChild (1); 
			canvasText.GetComponent<Text>().text = this.name;
		
			UIcanvas.GetComponent<PopUpTextSorter> ().popUpsList.Add (toSpawnCanvas);

			Destroy (gameObject);

		}
	}
}
