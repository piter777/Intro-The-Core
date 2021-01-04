using UnityEngine;

public class EnemtShot : MonoBehaviour
{

    public GameObject playerPosition;
    public Transform ThisShipPosition;
    public float fireRateRoketEnemy;
    private float nextFire;
    public float bulletSpeed = 10f;
    public GameObject RoketEnemy;
    // Use this for initialization
    void Start()
    {
        playerPosition = GameObject.FindWithTag("Player"); //take player pos from prefab

    }

    // Update is called once per frame
    void Update()
    {

        //Пли во врагам
        if (Time.time > nextFire)

        {
            nextFire = Time.time + fireRateRoketEnemy;


            //	var bullet = (GameObject)Instantiate(RoketEnemy, transform.position , Quaternion.Euler(0, 180, 0));
            var bullet = (GameObject)Instantiate(RoketEnemy, new Vector3(transform.position.x, playerPosition.transform.position.y, transform.position.z), transform.rotation);
            bullet.transform.LookAt(playerPosition.transform.position);

            bullet.transform.SetParent(GameObject.FindGameObjectWithTag("Dynamic").transform);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            //	bullet.GetComponent<Rigidbody> ().rotation=transform.rotation*  Quaternion.Euler(0, -90, 0);
            Destroy(bullet, 2.0f);

        }


    }
}
