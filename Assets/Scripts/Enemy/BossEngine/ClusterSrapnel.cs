using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterSrapnel : MonoBehaviour
{
    public GameObject playerPosition;
    // Position of boss.
    public Transform thisShipPosition;
    public float fireRateroketEnemy;
    private float nextFire;
    private float burstFire;
    // Weapon of boss.
    public GameObject roketEnemy;
    public int shootcount;
    public float atackspeed = 0.075f;
    public float bulletSpeed = 30;
    public float timeToExsplode = 1f;
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindWithTag("Player");
      
    }

    void Shot()
    {
        var bullet = (GameObject)Instantiate(roketEnemy, new Vector3(transform.position.x, 0.5f, transform.position.z), transform.rotation * Quaternion.Euler(0, Random.Range(-6f, 6f), 0));

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 30;
        Destroy(bullet, 4.0f);
    }

    private IEnumerator ChirleSoot()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        for (int i = 0; i < 1; i++)
        {
            //	Debug.Log ("i="+  i%2);  //1=1 3=1 5=1
            for (float j = 0; j < 360; j += 12)
            {
                float gradus = j;       
                var bullet = (GameObject)Instantiate(roketEnemy, new Vector3(transform.position.x, 0.5f, transform.position.z), transform.rotation * Quaternion.Euler(0, gradus, 0));
                bullet.transform.SetParent(GameObject.FindGameObjectWithTag("Dynamic").transform);
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 18;
                Destroy(bullet, 4.0f);
            }

            yield return wait;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeToExsplode -= Time.deltaTime;
        if (timeToExsplode < 0)
        {
            StartCoroutine(ChirleSoot());
            Destroy(this.gameObject);
        }
    }
}
