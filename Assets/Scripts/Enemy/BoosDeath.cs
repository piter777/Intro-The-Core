using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BoosDeath : MonoBehaviour
{


    public GameObject boos;
    public Scene scentoload;
    public GameObject bossRoom;
    public GameObject player;
    private GameObject dynamikChildrens;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }






    // Update is called once per frame
    void Update()
    {
        if (boos == null)
        {

            //	DontDestroyOnLoad (plyaer);
            //	SceneManager.LoadScene (2);
            DataHolder.Player = player;


            //Clean
            dynamikChildrens = GameObject.FindGameObjectWithTag("Dynamic");
            foreach (Transform child in dynamikChildrens.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            //Rebuild
            GameObject instance = Instantiate(bossRoom, new Vector3(0f, 0f, 0f), transform.rotation * Quaternion.Euler(0, 0, 0)) as GameObject;
            //GameObject parent = GameObject.FindGameObjectWithTag ("Dynamic").transform;
            instance.transform.SetParent(GameObject.FindGameObjectWithTag("Dynamic").transform);

            player.transform.position = new Vector3(50f, 1f, 50f);

            Destroy(this.gameObject);
        }
    }
}
