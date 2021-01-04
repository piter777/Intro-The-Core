using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunRuleSet : WeaponStats
{

    public int pelletsCount=8;
    
   
    public override IEnumerator FireProjective()
    {

        weaponBulletsInMagazine -= 1;

        for (int i = 0; i < pelletsCount; i++)
        {
            Accuracy = Random.Range(-(weaponAccuracy), weaponAccuracy);
            //Spawn a bulet on player position
            var bullet = (GameObject)Instantiate(this.gameObject, gameObject.transform.parent.transform.position, gameObject.transform.parent.transform.rotation * Quaternion.Euler(0, 0 + (-i + Accuracy), 0));
            bullet.SetActive(true);
            // Add velocity to the bullet
           // Debug.Log((Random.Range(-15f, 15f) / 100));
            float  shotweaponBulletSpeed = weaponBulletSpeed+ weaponBulletSpeed*( Random.Range(-10f, 10f)/100);

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * -shotweaponBulletSpeed;
            // change rotation to normal
            bullet.GetComponent<Rigidbody>().rotation = gameObject.transform.parent.transform.rotation * Quaternion.Euler(0, weaponRottation + 0+ Accuracy, 0);
            //Buffs
            bullet.GetComponent<WeaponStats>().weaponDamage = Mathf.RoundToInt(weaponDamage + (weaponDamage * gameObject.transform.parent.GetComponent<PlayerWeapons>().damageBuff / 100));
            // Destroy the bullet after 2 seconds
            Destroy(bullet, 1.0f);

        }
        yield return new WaitForSeconds(0);
    }
   




}