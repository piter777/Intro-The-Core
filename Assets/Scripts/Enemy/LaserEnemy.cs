using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : MonoBehaviour
{
    private GameObject player;
    private LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 2;
        RaycastHit hit;
        layerMask = ~layerMask;
        lr.SetPosition(0, transform.position);
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit))
        {
            lr.SetPosition(1, hit.point);
          
        }
        else { }
            

      
          
        
    }
}
