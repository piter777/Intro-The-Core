﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteTurning : MonoBehaviour
{
    public Transform playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPosition.position;
    }
}
