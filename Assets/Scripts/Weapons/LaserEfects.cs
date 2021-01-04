using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEfects : MonoBehaviour {
    public GameObject laserMain;
    public GameObject laserLigth;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (laserMain.gameObject.activeInHierarchy == false)
        {
            this.gameObject.SetActive(false);
            laserLigth.SetActive(false);
        }
    }
}
