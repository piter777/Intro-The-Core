using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMov : MonoBehaviour
{
    Transform player;
    UnityEngine.AI.NavMeshAgent nav;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    void Update()
    {
        // Give nav mesh agent new postion of player
        nav.SetDestination(player.position);

    }
}

