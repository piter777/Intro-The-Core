using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Shooter : BoosGeneralMechanick
{

  

    private IEnumerator AutomatedkSoot()
    {
        for (int i = 0; i < 15; i++)
        {
            WaitForSeconds wait = new WaitForSeconds(0.09f);
            Shot();
            yield return wait;
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



    private IEnumerator ConusWideSoot()

    {
        bool SideTurner = true;
        float gradus = -15;
        for (int i = 0; i < 45; i++)
        {
            WaitForSeconds wait = new WaitForSeconds(0.1f);



            if (SideTurner == true) { gradus = (gradus + 5); if (gradus >= 40) { SideTurner = false; } }
            if (SideTurner == false) { gradus = gradus - 5; if (gradus <= -40) { SideTurner = true; } }



            var bullet = (GameObject)Instantiate(roketEnemy, new Vector3(transform.position.x, playerPosition.transform.position.y, transform.position.z), transform.rotation * Quaternion.Euler(0, gradus, 0));
            //	bullet.transform.LookAt (playerPosition.transform.position);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 25;
            bullet.transform.SetParent(GameObject.FindGameObjectWithTag("Dynamic").transform);
            Destroy(bullet, 4.0f);
            yield return wait;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRateroketEnemy;
            int PaternRandomiser = Random.Range(0, 4);
            //	PaternRandomiser = 0;
            switch (PaternRandomiser)
            {
                case 0:
                //    StartCoroutine(ChaosstarShot());
                    break;
                case 1:
                    StartCoroutine(ShootgunSoot());
                    break;
                case 2:
                    StartCoroutine(ShootgunSoot());
                    break;
                case 3:
                    StartCoroutine(ConusWideSoot());
                    break;


                default: break;
            }

        }
    }
}
