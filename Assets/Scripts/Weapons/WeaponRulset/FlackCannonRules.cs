using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlackCannonRules : WeaponStats
{

    public int pelletsCount = 8;
    public GameObject shrapnel;

    private GameObject bullet;
    public override IEnumerator FireProjective()
    {

        weaponBulletsInMagazine -= 1;

        
            Accuracy = Random.Range(-(weaponAccuracy), weaponAccuracy);
            //Spawn a bulet on player position
            bullet = (GameObject)Instantiate(this.gameObject, gameObject.transform.parent.transform.position, gameObject.transform.parent.transform.rotation * Quaternion.Euler(0, 0 + (  Accuracy), 0));
            bullet.SetActive(true);
            // Add velocity to the bullet
            // Debug.Log((Random.Range(-15f, 15f) / 100));
            float shotweaponBulletSpeed = weaponBulletSpeed + weaponBulletSpeed * (Random.Range(-10f, 10f) / 100);

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * -shotweaponBulletSpeed;
            // change rotation to normal
            bullet.GetComponent<Rigidbody>().rotation = gameObject.transform.parent.transform.rotation * Quaternion.Euler(0, weaponRottation + 0 + Accuracy, 0);
            //Buffs
            bullet.GetComponent<WeaponStats>().weaponDamage = Mathf.RoundToInt(weaponDamage + (weaponDamage * gameObject.transform.parent.GetComponent<PlayerWeapons>().damageBuff / 100));
            // Destroy the bullet after 2 seconds
            Destroy(bullet, 0.41f);

        
        yield return new WaitForSeconds(0.30f);
        Flack();
    }
    private void Flack()
    {
        
        for (int i = 0; i < pelletsCount; i++)
        {
            Accuracy = Random.Range(-(75f), 75f);
            //Spawn a bulet on player position
            Debug.Log(Accuracy);
            var flack = (GameObject)Instantiate(shrapnel, bullet.transform.position, bullet.transform.rotation * Quaternion.Euler(0, 0 + (-i + Accuracy-90), 0));
            flack.SetActive(true);
            // Add velocity to the bullet
            // Debug.Log((Random.Range(-15f, 15f) / 100));
            float shotweaponBulletSpeed = weaponBulletSpeed + weaponBulletSpeed * (Random.Range(-10f, 10f) / 100);

            flack.GetComponent<Rigidbody>().velocity = flack.transform.forward * -shotweaponBulletSpeed;
            // change rotation to normal
            flack.GetComponent<Rigidbody>().rotation = gameObject.transform.parent.transform.rotation * Quaternion.Euler(0,  Accuracy, 0);
            //Buffs
            flack.GetComponent<WeaponStats>().weaponDamage = Mathf.RoundToInt(weaponDamage + (weaponDamage * gameObject.transform.parent.GetComponent<PlayerWeapons>().damageBuff / 100));
            // Destroy the bullet after 2 seconds
            Destroy(flack, 1.0f);

        }
    }





}