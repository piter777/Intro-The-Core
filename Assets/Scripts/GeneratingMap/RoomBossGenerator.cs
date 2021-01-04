using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBossGenerator : RoomDoorsSpawner
{
    public GameObject levelGenerator;
    public GameObject player;
    private GameObject dynamikChildrens;
    public GameObject[] BossPrefab;
    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Awake()
    {
        EnemysInRoom = EnemysPrefab.Length;

        enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject instance = Instantiate(BossPrefab[Random.Range(0, BossPrefab.Length)], this.transform.position, transform.rotation * Quaternion.Euler(0, 0, 0)) as GameObject;
        //GameObject parent = GameObject.FindGameObjectWithTag ("Dynamic").transform;

        instance.transform.SetParent(GameObject.FindGameObjectWithTag("Dynamic").transform);
        instance.SetActive(false);

        EnemysPrefab[0] = instance;
    }

    void Update()
    {
        if (EnemysPrefab[0] == null)
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
            GameObject instance = Instantiate(levelGenerator, new Vector3(0f, 0f, 0f), transform.rotation * Quaternion.Euler(0, 0, 0)) as GameObject;
            //GameObject parent = GameObject.FindGameObjectWithTag ("Dynamic").transform;
            instance.transform.SetParent(GameObject.FindGameObjectWithTag("Dynamic").transform);

            player.transform.position = new Vector3(50f, 1f, 50f);

            Destroy(this.gameObject);
        }
    }
}
