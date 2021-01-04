using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour {
   
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
        weaponBulletsInMagazine--;
        
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



}
