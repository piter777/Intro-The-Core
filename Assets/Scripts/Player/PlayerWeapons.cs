using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapons : MonoBehaviour
{

    // Position of player.
    public Transform plyerPosition;

    // Reload Speed.
    private float nextFire;


    // Inventory system.
    public List<GameObject> weaponsIninventory = new List<GameObject>();
    public GameObject curentWeapon;
    private WeaponStats weaponStats;
    public GameObject activItem;

    //Buffs
    public float damageBuff;
    public float atackSPeedBuff;


    private int numberOfWeapon = 0;
    //Lazer to spawn.
    private GameObject lazerTosapwn;
    public Image UIWeapon;
    public Image UIActiv;
    //	private int Ammo=100;
    private bool reloadActive = false;

    void Start()
    {

        weaponStats = curentWeapon.GetComponent<WeaponStats>();
        UIWeapon.sprite = curentWeapon.GetComponent<SpriteRenderer>().sprite;
    }

    public void FireWithRayWeapon()
    {
        lazerTosapwn = (GameObject)Instantiate(curentWeapon, plyerPosition.position, plyerPosition.rotation * Quaternion.Euler(0, 180, 0));
        lazerTosapwn.SetActive(true);
        GameObject player = GameObject.FindWithTag("player");
        lazerTosapwn.transform.parent = player.transform;

    }

    

    //Bullets.
    void FireWithAutomaticWeapon()
    {

        if (Time.time > nextFire && !reloadActive)
        {
            //Relaod Time
            nextFire = Time.time + (weaponStats.weaponReloadTime - weaponStats.weaponReloadTime * (atackSPeedBuff / 100));
            // get weapon stats
            int weaponBulletsInMagazine = weaponStats.weaponBulletsInMagazine;
            // if you have not WEAPON AMMO in magazine
            // reload mechanism
            if (weaponStats.weaponBulletsInMagazine <= 0)
            {
                weaponBulletsInMagazine = 0;
                float reloadTime = weaponStats.weaponMagazineReloadTime;
                StartCoroutine(Reload(reloadTime, curentWeapon));
            }

            // if you have ammo in magazine
            if (weaponBulletsInMagazine > 0)
            {
                StartCoroutine(weaponStats.FireProjective());
            }
        }

    }

    private IEnumerator Reload(float reloadTime, GameObject reloadedweapon)
    {
        
        WaitForSeconds wait = new WaitForSeconds(reloadTime);
        reloadActive = true;
        yield return wait;
        weaponStats.weaponBulletsInMagazine = weaponStats.weaponMagazine;
        reloadActive = false;
    }


    void Update()
    {

        // Activ Item Activator.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (activItem != null)
            {
                if (activItem.GetComponent<ActivDamageMetter>().damageNow >= activItem.GetComponent<ActivDamageMetter>().damageToActivate)
                {
                    activItem.GetComponent<ActivDamageMetter>().damageNow = 0;
                    var activatedItem = (GameObject)Instantiate(activItem, plyerPosition.position, plyerPosition.rotation);
                    activatedItem.SetActive(true);
                    activatedItem.transform.SetParent(gameObject.transform);
                }
            }
            //		activItem.GetComponent<IUsable> ().Activate();

        }
        //Manual reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            weaponStats.weaponBulletsInMagazine = 0;
            float reloadTime = weaponStats.weaponMagazineReloadTime;
            StartCoroutine(Reload(reloadTime, curentWeapon));

        }



        //scroll mouse
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            if (weaponsIninventory.Count > 1)


            {
                if (numberOfWeapon + 1 == weaponsIninventory.Count)
                {
                    numberOfWeapon = 0;
                }
                else
                {
                    numberOfWeapon++;
                }


                if (weaponStats.weaponType == 1)
                {
                    curentWeapon.SetActive(false);
                }

                curentWeapon = weaponsIninventory[numberOfWeapon];
                weaponStats = curentWeapon.GetComponent<WeaponStats>();

                UIWeapon.sprite = curentWeapon.GetComponent<SpriteRenderer>().sprite;

            }



        }



        //semi automatic weapon
        if (Input.GetKeyDown(KeyCode.Mouse0) == true)
        {


            if (weaponStats.weaponType == 2)
            {

                FireWithAutomaticWeapon();
            }

        }
        //FIRE weapon
        if (Input.GetKey(KeyCode.Mouse0) == true && !reloadActive)
        {
            // Weapon with magazine
            if (weaponStats.weaponType == 0)
            {
                FireWithAutomaticWeapon();
            }

            //Ray weapon

            if ((weaponStats.weaponType == 1) && (weaponStats.weaponBulletsInMagazine >= 0))
            {
                curentWeapon.SetActive(true);
                curentWeapon.transform.rotation = this.transform.rotation * Quaternion.Euler(0, 180, 0);

                if (weaponStats.weaponBulletsInMagazine <= 0)
                {
                    float reloadTime = weaponStats.weaponMagazineReloadTime;
                    StartCoroutine(Reload(reloadTime, curentWeapon));
                }

                if (Time.time > nextFire)
                {
                    weaponStats.weaponBulletsInMagazine--;
                    nextFire = Time.time + (weaponStats.weaponReloadTime - (weaponStats.weaponReloadTime * atackSPeedBuff / 100));

                }


            }
            else
            {
                if (weaponStats.weaponBulletsInMagazine <= 0)
                {
                    curentWeapon.SetActive(false);
                }
            }
        }





        if (Input.GetKeyUp(KeyCode.Mouse0) == true)
        {

            curentWeapon.SetActive(false);

        }
        //Actic weapon dysplay.	
        if (activItem != null)
        {
            UIActiv.sprite = activItem.GetComponent<SpriteRenderer>().sprite;
        }
    }
    public void AddItem(GameObject itemToadd)
    {
        weaponsIninventory.Add(itemToadd);
    }






}
