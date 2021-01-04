using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour {


	public GameObject PauseMenuObject;
	public bool Ispause;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if (Ispause)
			{
				ResumeGame ();
			} 
			else
			{Ispause = true;
				PauseMenuObject.SetActive (true);
				Time.timeScale = 0f;
			}


			//Application.Quit();//exit
		}
		
	}

	public void ResumeGame()
	{
		Ispause = false;
		PauseMenuObject.SetActive (false);
		Time.timeScale = 1f;
		
	}
	public void ReturnTomain()
	{
		SceneManager.LoadScene (0);
		ResumeGame ();
	}
}
