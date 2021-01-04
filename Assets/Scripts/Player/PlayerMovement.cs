using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forseMult = 20;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //    camera = Camera.main;


    }
    private void Start()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray camRayposition = Camera.main.ScreenPointToRay(transform.position);
        float rotZ = camRay.origin.z - camRayposition.origin.z;

    }

    void Update()
    {
        // Movment
        float moveh = Input.GetAxisRaw("Horizontal");
        float movev = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(moveh, 0f, movev);
        rb.velocity = movement * forseMult;
        // Debug.Log(rb.velocity);


        //    if(Camera.main.orthographic)
        //  OstograficCameraRotation90();
        //    OstograficCameraRotation60();
        //    else
        //    PerspectiveCmaeraRotation();

    }

    public void PerspectiveCmaeraRotation()
    { //Turning
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    Vector3 secondCamera = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 turnDir = new Vector3(Input.GetAxisRaw("Mouse X"), 0f, Input.GetAxisRaw("Mouse Y"));
        //  Debug.Log(turnDir);
        turnDir = camRay.direction * -1f;
        //   Debug.Log(camRay + " " + secondCamera);

        if (turnDir != Vector3.zero)
        {
            Vector3 playerToMouse = (transform.position + turnDir) - transform.position;
            playerToMouse.y = 1f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
        }
    }
    public void OstograficCameraRotation90()
    {

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 targetPosition = new Vector3(camRay.origin.x, transform.position.y, camRay.origin.z);

        transform.LookAt(2 * transform.position - targetPosition);

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
