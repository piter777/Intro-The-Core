using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosGeneralMechanick : MonoBehaviour
{

    public GameObject playerPosition;
    // Position of boss.
    public Transform thisShipPosition;
    public float fireRateroketEnemy;
   protected float nextFire;
    protected float burstFire;
    // Weapon of boss.
    public GameObject roketEnemy;
    public int shootcount;
    public float atackspeed = 0.075f;
    public float bulletSpeed = 30;

    void Start()
    {
        playerPosition = GameObject.FindWithTag("Player");

    }

  protected  void Shot()
    {
        var bullet = (GameObject)Instantiate(roketEnemy, new Vector3(transform.position.x, playerPosition.transform.position.y, transform.position.z), transform.rotation * Quaternion.Euler(0, Random.Range(-6f, 6f), 0));

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 30;
        Destroy(bullet, 4.0f);
    }
}
