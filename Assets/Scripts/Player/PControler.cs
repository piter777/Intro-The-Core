using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PControler : MonoBehaviour
{

    // Position of player.
    public Transform plyerPosition;

    // Reload Speed.
    private float nextFire;

    // Inventory system.
    public List<GameObject> weaponsIninventory = new List<GameObject>();
    public GameObject curentWeapon;
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
        //	DataHolder.playerHealth = 3;
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
            nextFire = Time.time + (curentWeapon.GetComponent<WeaponStats>().weaponReloadTime - curentWeapon.GetComponent<WeaponStats>().weaponReloadTime * (atackSPeedBuff / 100));
            // get weapon stats
            int weaponBulletsInMagazine = curentWeapon.GetComponent<WeaponStats>().weaponBulletsInMagazine;
            // if you have not WEAPON AMMO in magazine
            // reload mechanism
            if (curentWeapon.GetComponent<WeaponStats>().weaponBulletsInMagazine <= 0)
            {
                float reloadTime = curentWeapon.GetComponent<WeaponStats>().weaponMagazineReloadTime;
                StartCoroutine(Reload(reloadTime, curentWeapon));
            }

            // if you have ammo in magazine
            if (weaponBulletsInMagazine > 0)
            {
                curentWeapon.GetComponent<WeaponStats>().weaponBulletsInMagazine--;
                //Spawn a bulet on player position
                var bullet = (GameObject)Instantiate(curentWeapon, plyerPosition.position, plyerPosition.rotation * Quaternion.Euler(0, 0, 0));
                bullet.SetActive(true);
                // Add velocity to the bullet
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * -curentWeapon.GetComponent<WeaponStats>().weaponBulletSpeed;
                // change rotation to normal
                	bullet.GetComponent<Rigidbody> ().rotation = plyerPosition.rotation * Quaternion.Euler (0, curentWeapon.GetComponent<WeaponStats>().weaponRottation+0, 0);
                //Buffs
                bullet.GetComponent<WeaponStats>().weaponDamage = Mathf.RoundToInt(bullet.GetComponent<WeaponStats>().weaponDamage + (bullet.GetComponent<WeaponStats>().weaponDamage * damageBuff / 100));
                // Destroy the bullet after 2 seconds
                Destroy(bullet, 2.0f);
            }



        }

    }

    private IEnumerator Reload(float reloadTime, GameObject reloadedweapon)
    {
        WaitForSeconds wait = new WaitForSeconds(reloadTime);
        reloadActive = true;
        yield return wait;
        curentWeapon.GetComponent<WeaponStats>().weaponBulletsInMagazine = curentWeapon.GetComponent<WeaponStats>().weaponMagazine;
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



        //scroll mouse
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            if (weaponsIninventory.Count > 1)


            {
                if (numberOfWeapon + 1 == weaponsIninventory.Count)
                { numberOfWeapon = 0; }
                else
                { numberOfWeapon++; }


                if (curentWeapon.GetComponent<WeaponStats>().weaponType == 1) curentWeapon.SetActive(false);
                curentWeapon = weaponsIninventory[numberOfWeapon];

                UIWeapon.sprite = curentWeapon.GetComponent<SpriteRenderer>().sprite;

            }



        }


        if (Input.GetButton("Fire1"))
        {
            // Weapon with magazine
            if (curentWeapon.GetComponent<WeaponStats>().weaponType == 0) { FireWithAutomaticWeapon(); }


        }


        //Ray weapon
        if (Input.GetKey(KeyCode.Mouse0) == true && !reloadActive)
        {
            if ((curentWeapon.GetComponent<WeaponStats>().weaponType == 1) && (curentWeapon.GetComponent<WeaponStats>().weaponBulletsInMagazine >= 0))
            {
                curentWeapon.SetActive(true);
                curentWeapon.transform.rotation = this.transform.rotation * Quaternion.Euler(0, 180, 0);

                if (curentWeapon.GetComponent<WeaponStats>().weaponBulletsInMagazine <= 0)
                {
                    float reloadTime = curentWeapon.GetComponent<WeaponStats>().weaponMagazineReloadTime;
                    StartCoroutine(Reload(reloadTime, curentWeapon));
                }

                if (Time.time > nextFire)
                {
                    curentWeapon.GetComponent<WeaponStats>().weaponBulletsInMagazine--;
                    nextFire = Time.time + (curentWeapon.GetComponent<WeaponStats>().weaponReloadTime - (curentWeapon.GetComponent<WeaponStats>().weaponReloadTime * atackSPeedBuff / 100));
                   
                }


            }
            else
            {
                if (curentWeapon.GetComponent<WeaponStats>().weaponBulletsInMagazine <= 0)
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
            UIActiv.sprite = activItem.GetComponent<SpriteRenderer>().sprite;


    }
    public void AddItem(GameObject itemToadd)
    {
        weaponsIninventory.Add(itemToadd);
    }






}
