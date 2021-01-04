using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour {
	public Scene sceneits;
	private GameObject dynamikChildrens;
	public GameObject mapGenerator;
	public GameObject player;

	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player");
	}


	public void StartGame() {
		SceneManager.LoadScene (1);
	}
	public void ExitGame(){Application.Quit();}

	
	// Update is called once per frame
	void Update () {


       

		if(Input.GetKeyDown(KeyCode.Escape))
		{
                



		}
		if(Input.GetKeyDown(KeyCode.U))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //restart the game
		}

		if (Input.GetKeyDown (KeyCode.O)) {


			if (SceneManager.GetActiveScene ().buildIndex == 0) {SceneManager.LoadScene (0);}
			if (SceneManager.GetActiveScene ().buildIndex == 1) {SceneManager.LoadScene (0);}


		}
		//Cleean TheBoard
		if (Input.GetKeyDown (KeyCode.H)) {

			dynamikChildrens = GameObject.FindGameObjectWithTag("Dynamic");
			foreach (Transform child in dynamikChildrens.transform) 
			{
				GameObject.Destroy (child.gameObject);
			}

		}
		if (Input.GetKeyDown (KeyCode.J)) 
		{

		//	GameObject instance = Instantiate (mapGenerator, new Vector3 (0f,0f,0f), Quaternion.identity) as GameObject;
			player.transform.position = new Vector3 (50f,1f,50f);


		}
	/*	if (Input.GetKeyDown (KeyCode.I)) {


			if (invTrial == true) {
				inventory.SetActive (false);
				invTrial = false;
			} else {inventory.SetActive (true);
				invTrial = true;
			}
			 



		}*/


		 


		/*overlay.SetActive (false);
		AudioListener.volume = 1f;
		Time.timeScale = 1f;*/
		
	}
}
