using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRendere : MonoBehaviour
{
    public int maxReflectionCount = 5;
    private GameObject player;
    public GameObject laserLight;
    public int currentBouse = 1;
    private LineRenderer lr;
    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        laserLight.transform.SetParent(this.transform.parent);
        lr.positionCount = 2;


    }
    private float nextFire;
    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, transform.position);
        if (maxReflectionCount > 1)
        {

            currentBouse = 1;
            lr.positionCount = 2;
            
            DrawBounseLine(transform.position, transform.forward, maxReflectionCount);
        }
        else
            DrawLine(transform.position, transform.forward);


        //Debug.Log (laserLight.transform.localPosition+" "+lr.GetPosition (1)+""+laserLight.transform.TransformPoint (lr.GetPosition (1)));
    }

    void DrawBounseLine(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {

        RaycastHit hit;

        if (Physics.Raycast(position, direction, out hit))
        {
            if ((hit.collider) && (hit.collider.tag != "Trigger"))
            {
      
                //   lr.SetPosition(1, Vector3.Lerp(hit.point, gameObject.transform.position,0));
                //new Vector3(hit.point.x+Random.Range(-5f,5f), hit.point.y, hit.point.z + Random.Range(-5f, 5f)));
                lr.SetPosition(currentBouse, hit.point);
                //  lr.numPositions = 4;


                direction = Vector3.Reflect(direction, hit.normal);
                position = hit.point;
                currentBouse++;
                if (reflectionsRemaining == 1)
                {
                    return;
                }
                lr.positionCount++;
                DrawBounseLine(position, direction, reflectionsRemaining - 1);

              
                if (((hit.collider.tag == "Enemy") || (hit.collider.tag == "Projective")) && (Time.time > nextFire))
                {

                    Debug.Log("laser hit enemyaw");
                    //Relaod Time
                    nextFire = Time.time + (this.GetComponent<WeaponStats>().weaponReloadTime - (this.GetComponent<WeaponStats>().weaponReloadTime * player.GetComponent<PlayerWeapons>().atackSPeedBuff / 100));
                    EnemyHealth healthEnemy = hit.collider.gameObject.GetComponent<EnemyHealth>();

                    //Deal Damage
                    if (healthEnemy != null)
                    {
                        float DamageBuffAdd = player.GetComponent<PlayerWeapons>().damageBuff;
                        float DamageTOAdd = this.GetComponent<WeaponStats>().weaponDamage + (this.GetComponent<WeaponStats>().weaponDamage * DamageBuffAdd / 100);
                        healthEnemy.DamageTaken(DamageTOAdd);
                    }



                }
            }
            else
            {
                position += direction * 300;
                //laserLight.transform.position = hit.transform.position;
                lr.SetPosition(currentBouse, direction * 2000);
            }
        }
        laserLight.SetActive(true);
        laserLight.transform.position = new Vector3(lr.GetPosition(currentBouse-1).x, 1f, lr.GetPosition(currentBouse-1).z);
    }



    void DrawLine(Vector3 position, Vector3 direction)
    {
        RaycastHit hit;
        LayerMask layerMask = 2;
        layerMask = ~layerMask;
          if (Physics.Raycast(position, direction, out hit, Mathf.Infinity))
    
        {
            if ((hit.collider) && (hit.collider.tag != "Trigger"))
            {
                Debug.Log("layer " + hit.transform.gameObject.layer);
                //   lr.SetPosition(1, Vector3.Lerp(hit.point, gameObject.transform.position,0));
                //new Vector3(hit.point.x+Random.Range(-5f,5f), hit.point.y, hit.point.z + Random.Range(-5f, 5f)));
                lr.SetPosition(1, hit.point);
                //  lr.numPositions = 4;


                if (((hit.collider.tag == "Enemy") || (hit.collider.tag == "Projective")) && (Time.time > nextFire))
                {
                    //Relaod Time
                    nextFire = Time.time + (this.GetComponent<WeaponStats>().weaponReloadTime - (this.GetComponent<WeaponStats>().weaponReloadTime * player.GetComponent<PlayerWeapons>().atackSPeedBuff / 100));
                    EnemyHealth healthEnemy = hit.collider.gameObject.GetComponent<EnemyHealth>();

                    //Deal Damage
                    if (healthEnemy != null)
                    {
                        float DamageBuffAdd = player.GetComponent<PlayerWeapons>().damageBuff;
                        float DamageTOAdd = this.GetComponent<WeaponStats>().weaponDamage + (this.GetComponent<WeaponStats>().weaponDamage * DamageBuffAdd / 100);
                        healthEnemy.DamageTaken(DamageTOAdd);
                    }



                }
            }
            else
            {
                //  position += direction * 300;
                //laserLight.transform.position = hit.transform.position;
                lr.SetPosition(1, direction * 2000);
            }
        }
        laserLight.SetActive(true);
        laserLight.transform.position = new Vector3(lr.GetPosition(1).x, 1f, lr.GetPosition(1).z);
    }
}
