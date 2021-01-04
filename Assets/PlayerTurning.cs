using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OstograficCameraRotation60();
    }
    public void OstograficCameraRotation60()
    {


        Vector3 worldPosition = Vector3.zero;
        Plane plane = new Plane(Vector3.up, -1);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }
        transform.LookAt(2 * transform.position - worldPosition);

    }
}
