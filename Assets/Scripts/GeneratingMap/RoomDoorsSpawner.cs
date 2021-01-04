using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoorsSpawner : MonoBehaviour
{



    public GameObject[] DoorsPrefab;
    public List<GameObject> fogOfWar = new List<GameObject>();


    public bool DoorActive = false;
    public GameObject[] EnemysPrefab;
    public int EnemysInRoom;
    public Material MaterialGreen;
    private bool[] allTimeClosedDoors = new bool[] { false, false, false, false };

    // Use this for initialization
    void Start()
    {
        //EnemysInRoom= EnemysPrefab.Length;

        //	enabled = false;


    }
    void Awake()
    {
        EnemysInRoom = EnemysPrefab.Length;

        enabled = false;
    }



    public void CloseDors()
    {

        if (DoorActive == false)
        {
            DoorActive = true;
            enabled = true;

            for (int i = 0; i < fogOfWar.Count; i++)
            {
                if (fogOfWar[i] != null) fogOfWar[i].SetActive(false);
            }


            for (int i = 0; i < DoorsPrefab.Length; i++)
            {
                //			if (DoorsPrefab [i].active) 
                if (DoorsPrefab[i].activeInHierarchy)
                { //  Debug.Log (i + " door is closed");
                    allTimeClosedDoors[i] = true;
                }
                DoorsPrefab[i].SetActive(true);
            }
            //	for (int i = 0; i < SpawnPointPrefab.Length; i++) { Debug.Log ("SPAWN");}
            //	for (int i=0; i < SpawnPointPrefab.Length; i++){var Enemy = (GameObject)Instantiate (EnemyPrefab, SpawnPointPrefab[i].transform.position, SpawnPointPrefab[i].transform.rotation );}
            for (int i = 0; i < EnemysPrefab.Length; i++) { EnemysPrefab[i].SetActive(true); }
        }
    }



    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < EnemysPrefab.Length; i++)
        {
            if (EnemysPrefab[i] == null)
                EnemysInRoom--;
        }

        if ((EnemysInRoom == 0) || (EnemysPrefab.Length == 0))
        {

            //	ColoRPrefab.GetComponent<Renderer> ().material = MaterialGreen;
            enabled = false;
            for (int i = 0; i < DoorsPrefab.Length; i++)
            {
                if (allTimeClosedDoors[i] != true)
                    DoorsPrefab[i].SetActive(false);


            }

        }

        EnemysInRoom = EnemysPrefab.Length;

        //	Debug.Log (Enemys.Length);


    }








}
