using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpTextSorter : MonoBehaviour {
	
	public List<GameObject> popUpsList = new List<GameObject> (); 
	//float timer=3;
	//float timeNow=0;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*timeNow = Time.deltaTime + timeNow;
		if (timeNow > timer)
			timeNow = 0;
		Debug.Log("timeNow="+timeNow+" Time.deltaTime="+Time.deltaTime);*/
		if (popUpsList.Count > 0) 
		{	
			Destroy (popUpsList[0],5f);
			if (popUpsList [0] != null)
			{
				popUpsList [0].SetActive (true);
//				Debug.Log (popUpsList [0]);
			}
				
			else
				popUpsList.RemoveAt (0);
			
			if (popUpsList.Count > 1) 
			{
				for (int i = 1; i < popUpsList.Count; i++)
				{
					popUpsList [i].SetActive (false);
				}
			}
		}

		
	}



}
