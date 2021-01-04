using UnityEngine;

public class TurretRotation : MonoBehaviour
{

    public GameObject playerPosition;
    // Update is called once per frame
    private void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {







        transform.LookAt(playerPosition.transform.position);


    }
}
