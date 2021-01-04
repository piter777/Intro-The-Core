using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivDamageMetter : MonoBehaviour
{
    public int damageToActivate;
    public int damageNow;



    public virtual IEnumerator Activate()
    {
        yield return 0f;
    }
}
