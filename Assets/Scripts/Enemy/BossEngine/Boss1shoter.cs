using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class Boss1shoter : BoosGeneralMechanick
{


   

    

	

	private	IEnumerator AutomatedkSoot()
	{ for (int i = 0; i < 15; i++) 
		{WaitForSeconds wait = new WaitForSeconds( 0.09f ) ;
			Shot ();
			yield return wait ;
		}
	}


	
    private IEnumerator ChaosstarShot()
    {
        for (int i = 0; i < 15; i++)
        {
            WaitForSeconds wait = new WaitForSeconds(0.15f);
            float gradus = 0;
            for (int j = 0; j < 8; j++)
            {

                var bullet = (GameObject)Instantiate(roketEnemy, new Vector3(transform.position.x, playerPosition.transform.position.y, transform.position.z), Quaternion.Euler(0, gradus, 0));
                //	bullet.transform.LookAt (playerPosition.transform.position);
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 25;
                bullet.transform.SetParent(GameObject.FindGameObjectWithTag("Dynamic").transform);
                Destroy(bullet, 4.0f);
                gradus += 45;
            }
            yield return wait;
        }
    }

    private	IEnumerator ConusSoot()
	{WaitForSeconds wait = new WaitForSeconds( 1.1f ) ;
		for (int i = 0; i < 3; i++) 
		{ 
			//	Debug.Log ("i="+  i%2);  //1=1 3=1 5=1
			for(float j=-90;j<90;j+=8)
			{	float gradus = j;
				if (i % 2 == 1) { gradus += 2;
				}
				var bullet = (GameObject)Instantiate(roketEnemy, new Vector3(transform.position.x, playerPosition.transform.position.y, transform.position.z)  , transform.rotation* Quaternion.Euler(0, gradus, 0));
				bullet.transform.SetParent (GameObject.FindGameObjectWithTag ("Dynamic").transform);
                

                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 18;
				Destroy(bullet, 4.0f);
			}

			yield return wait ;
		}
	}

	private	IEnumerator ChirleSoot()
	{
		WaitForSeconds wait = new WaitForSeconds( 1f ) ;
		for (int i = 0; i < 3; i++) 
		{ 
		//	Debug.Log ("i="+  i%2);  //1=1 3=1 5=1
			for(float j=0;j<360;j+=12)
			{	float gradus = j;
				if (i % 2 == 1) { gradus += 5;
				}
				var bullet = (GameObject)Instantiate(roketEnemy, new Vector3(transform.position.x, playerPosition.transform.position.y, transform.position.z), transform.rotation* Quaternion.Euler(0, gradus, 0));
				bullet.transform.SetParent (GameObject.FindGameObjectWithTag ("Dynamic").transform);
				bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 18;
				Destroy(bullet, 4.0f);
			}
		
			yield return wait ;
		}
	}

	void Update () 
	{
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRateroketEnemy;
			int PaternRandomiser=Random.Range (0,4);
			switch (PaternRandomiser)
			{
			case 0:
				StartCoroutine(	AutomatedkSoot ());
				break;
			case 1:
				StartCoroutine(	ChirleSoot ());
				break;
			case 2:
				StartCoroutine(	ConusSoot ());
				break;
			case 3:
				StartCoroutine(ChaosstarShot());
				break;
			

			default: break;
			}
	
		}
	}

}






