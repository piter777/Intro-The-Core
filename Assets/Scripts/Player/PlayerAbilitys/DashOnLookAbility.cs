using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashOnLookAbility : Ability
{
    public override IEnumerator UseAbility()
    {
        float t = 0;
        Vector3 start = transform.position;
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 turnDir = new Vector3(Input.GetAxisRaw("Mouse X"), 0f, Input.GetAxisRaw("Mouse Y"));
        //get precentage of turing
        float xprecentage;
        float zprecentage;
        float precent;
        precent = (Mathf.Abs(camRay.direction.x) + Mathf.Abs(camRay.direction.z)) / 100;
        xprecentage = camRay.direction.x / precent;
        zprecentage = camRay.direction.z / precent;
        //now we turn in equal directions
        turnDir = camRay.direction * 1f;
        while (t <= abilityTime)
        {
            t += Time.fixedDeltaTime;
            GetComponent<Rigidbody>().velocity = new Vector3(xprecentage, 0, zprecentage) * 0.5f;
            yield return null;
        }
    }
}
