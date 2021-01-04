using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassivePickUp : MonoBehaviour {
	public int damagePassive;
	public int atackSPeedPassive;
	public GameObject canvas;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {




	}

	private void OnTriggerEnter(Collider other)
	{   
		if (other.tag == "Player") 
		{
            PlayerWeapons local =other.GetComponent<PlayerWeapons>();
			local.damageBuff += damagePassive;
			local.atackSPeedBuff += atackSPeedPassive;
			Destroy (gameObject);
			//spawn a canvas.


			GameObject	toSpawnCanvas = Instantiate (canvas, new Vector3(0,100,0), Quaternion.Euler (0, 0, 0) );
			GameObject UIcanvas = GameObject.FindGameObjectWithTag ("UI");
			toSpawnCanvas.transform.SetParent (UIcanvas.transform,false);
			var	canvasText = toSpawnCanvas.transform.GetChild (1); //("PikableWeaponText");
			canvasText.GetComponent<Text> ().text = this.name;
//			var	canvasImage = toSpawnCanvas.transform.GetChild (2);
		//	canvasImage.GetComponent<Image>().sprite = WeaponToInventory.GetComponent<SpriteRenderer>().sprite;
			UIcanvas.GetComponent<PopUpTextSorter> ().popUpsList.Add (toSpawnCanvas);

		}
	}
}
