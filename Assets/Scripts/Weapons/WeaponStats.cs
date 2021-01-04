using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour {
    public string weaponName="Weapon";
    public int weaponType;
	public int weaponDamage ;
	public float weaponReloadTime;
	public int weaponMagazine;
	public int weaponBulletsInMagazine ;
	public float weaponMagazineReloadTime ;
	public float weaponBulletSpeed ;
    public float weaponRottation;
    public float weaponAccuracy;//0 best 100 worst
    protected float Accuracy;


  

    public virtual IEnumerator FireProjective()
    {
        Accuracy = Random.Range(-(weaponAccuracy), weaponAccuracy);
        if (weaponBulletsInMagazine <= 0)
            weaponBulletsInMagazine = 0;

        //Spawn a bulet on player position
        var bullet = (GameObject)Instantiate(this.gameObject, gameObject.transform.parent.transform.position, gameObject.transform.parent.transform.rotation * Quaternion.Euler(0, 0+ Accuracy, 0));
        bullet.SetActive(true);
        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * -weaponBulletSpeed;
        // change rotation to normal
        bullet.GetComponent<Rigidbody>().rotation = gameObject.transform.parent.transform.rotation * Quaternion.Euler(0, weaponRottation + 0+ Accuracy, 0);
        //Buffs
        bullet.GetComponent< WeaponStats>().weaponDamage = Mathf.RoundToInt(weaponDamage + (weaponDamage * gameObject.transform.parent.GetComponent<PlayerWeapons>().damageBuff / 100));
        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
        yield return new WaitForSeconds(0);
    }
    // Destroy by contact
    protected GameObject player;
    protected GameObject contactedActivItem;

    protected virtual void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {

            int thisWeaponDamgae = this.GetComponent<WeaponStats>().weaponDamage;
            EnemyHealth healthEnemy = other.gameObject.GetComponent<EnemyHealth>();

            if (healthEnemy != null)
            {
                healthEnemy.DamageTaken(thisWeaponDamgae);
                
            }
            player = GameObject.FindGameObjectWithTag("Player");

            if (player.GetComponent<PlayerWeapons>().activItem != null)
            {

                contactedActivItem = player.GetComponent<PlayerWeapons>().activItem;
                contactedActivItem.GetComponent<ActivDamageMetter>().damageNow += thisWeaponDamgae;
            }


            Destroy(gameObject);


        }

        if (other.tag == "Room")
        {
            Destroy(gameObject);
        }



    }

}
