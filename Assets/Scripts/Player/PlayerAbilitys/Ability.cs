using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    protected Rigidbody rb;
    public float abilityTime = 0.5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
   

 

    public virtual IEnumerator UseAbility()
    {
     
        yield return null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
         
            StartCoroutine(UseAbility());
         
        }
    }
}
