using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevWeaponRules : WeaponStats
{


    public override IEnumerator FireProjective()
    {

      
       
        for (int i = 0; i < 3; i++)
        {
            weaponBulletsInMagazine--;
            if (weaponBulletsInMagazine <= 0)
                weaponBulletsInMagazine = 0;
Accuracy = Random.Range(-(weaponAccuracy), weaponAccuracy);
            //Spawn a bulet on player position
            var bullet = (GameObject)Instantiate(this.gameObject, gameObject.transform.parent.transform.position, gameObject.transform.parent.transform.rotation * Quaternion.Euler(0, 0 + (-15 + i * 15) + Accuracy, 0));
            bullet.SetActive(true);
            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * -weaponBulletSpeed;
            // change rotation to normal
            bullet.GetComponent<Rigidbody>().rotation = gameObject.transform.parent.transform.rotation * Quaternion.Euler(0, weaponRottation + 0 + Accuracy, 0);
            //Buffs
            bullet.GetComponent<WeaponStats>().weaponDamage = Mathf.RoundToInt(weaponDamage + (weaponDamage * gameObject.transform.parent.GetComponent<PlayerWeapons>().damageBuff / 100));
            // Destroy the bullet after 2 seconds
            Destroy(bullet, 2.0f);
            
        }
        yield return new WaitForSeconds(0);
    }




}
