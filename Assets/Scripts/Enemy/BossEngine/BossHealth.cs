using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    public GameObject[] armorPlates;
    private float healthParts;
    private int plateNumber=0;
    private void Start()
    {
        healthParts = startingHealth / (armorPlates.Length+1);
    }

    public override void DamageTaken(float WeaponDamage)
    {
        //destroy armor plates
        if ((currentHealth < startingHealth - healthParts* (plateNumber+1)) &&(armorPlates!=null))
        {
            Destroy(armorPlates[plateNumber]);
            plateNumber++;
           
        }


        currentHealth -= WeaponDamage;
        if (currentHealth <= 0)

        {

            Destroy(gameObject);
            //	DataHolder.Score++;
        }


    }
}
