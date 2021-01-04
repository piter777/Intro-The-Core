using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyShotCollider : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter (Collider other) { 
		//Debug.Log ("enemy roket colided");
		if (other.tag == "Player"  )
		{
			Destroy (gameObject);


			PlayerHealth healthPlayer =other.gameObject.GetComponent<PlayerHealth>();

			if(healthPlayer != null){healthPlayer.DamageTaken ();}


		/*	DataHolder.PlayerHealth--;
			if (DataHolder.PlayerHealth < 1) 
			{
				DataHolder.Score = 0;
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //restart the game
			}*/

		}
        
        if (other.tag == "Room") {
			Destroy (gameObject);
		}

}
    void OnCollisionEnter(Collision coll)
    {
    //    if (coll.gameObject.tag == "Room")   Destroy(gameObject);

    }



}