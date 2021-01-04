using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boos3Shotter : BoosGeneralMechanick
{

    public GameObject specialRoketEnemy;
    public GameObject turet;
 

    private IEnumerator AutomatedkShrapnelSoot()
    {
        for (int i = 0; i < 3; i++)
        {
            // Shot();

            var bullet = (GameObject)Instantiate(specialRoketEnemy, new Vector3(transform.position.x, playerPosition.transform.position.y, transform.position.z), transform.rotation * Quaternion.Euler(0, Random.Range(-6f, 6f), 0));

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;
            Destroy(bullet, 4.0f);
            yield return new WaitForSeconds(0.09f);
        }
    }


    private IEnumerator ShootgunSoot()
    {
        for (int i = 0; i < 4; i++)
        {
            WaitForSeconds wait = new WaitForSeconds(0.1f);
            for (int j = 0; j < 8; j++)
            {
                var bullet = (GameObject)Instantiate(roketEnemy, new Vector3(transform.position.x, playerPosition.transform.position.y, transform.position.z), transform.rotation * Quaternion.Euler(0, Random.Range(-10f, 10f), 0));

                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 35;
                Destroy(bullet, 4.0f);
            }


            yield return wait;
        }
    }

    private IEnumerator SpawnTurret() {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        var turet1 = (GameObject) Instantiate(turet, new Vector3(transform.position.x + 2, playerPosition.transform.position.y, transform.position.z), transform.rotation * Quaternion.Euler(0, Random.Range(-10f, 10f), 0));
        var turet2 = (GameObject) Instantiate(turet, new Vector3(transform.position.x - 2, playerPosition.transform.position.y, transform.position.z), transform.rotation * Quaternion.Euler(0, Random.Range(-10f, 10f), 0));
        // bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 35;
        Destroy(turet1, 30.0f);
        Destroy(turet2, 30.0f);


        yield return wait;
    }



    private IEnumerator ConusWideSoot()

    {
        bool SideTurner = true;
        float gradus = -15;
        for (int i = 0; i < 45; i++)
        {
            if (SideTurner) {
                gradus = (gradus + 5);
                if (gradus >= 40) {
                    SideTurner = false;
                }
            }

            if (!SideTurner) {
                gradus = gradus - 5;
                if (gradus <= -40) {
                    SideTurner = true;
                }
            }

            var bullet = (GameObject)Instantiate(roketEnemy, new Vector3(transform.position.x, playerPosition.transform.position.y, transform.position.z), transform.rotation * Quaternion.Euler(0, gradus, 0));
            //	bullet.transform.LookAt (playerPosition.transform.position);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 25;
            bullet.transform.SetParent(GameObject.FindGameObjectWithTag("Dynamic").transform);
            Destroy(bullet, 4.0f);
            yield return new WaitForSeconds(0.1f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRateroketEnemy;
            switch (Random.Range(0, 4))
            {
                case 0:
                    StartCoroutine(AutomatedkShrapnelSoot());
                    break;
                case 1:
                    StartCoroutine(SpawnTurret());
                    break;
                case 2:
                    StartCoroutine(ShootgunSoot());
                    break;
                case 3:
                    StartCoroutine(ConusWideSoot());
                    break;
            }

        }
    }
}
