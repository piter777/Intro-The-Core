using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformPosition : MonoBehaviour {
    public float speed = 5;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        float moveh = Input.GetAxisRaw("Horizontal");
        float movev = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(moveh, 0f, movev);
        transform.position += movement * Time.deltaTime * speed;
    }
}