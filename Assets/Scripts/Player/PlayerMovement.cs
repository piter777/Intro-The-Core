using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forseMult = 20;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();



    }

    void Update()
    {
        // Movment
        float moveh = Input.GetAxisRaw("Horizontal");
        float movev = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(moveh, 0f, movev);
        rb.velocity = movement * forseMult;
       // Debug.Log(rb.velocity);
        //Turning
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 turnDir = new Vector3(Input.GetAxisRaw("Mouse X"), 0f, Input.GetAxisRaw("Mouse Y"));

        turnDir = camRay.direction * -1f;

        if (turnDir != Vector3.zero)
        {
            Vector3 playerToMouse = (transform.position + turnDir) - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
        }




    }
}
