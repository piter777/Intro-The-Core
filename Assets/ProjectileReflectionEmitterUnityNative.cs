using UnityEditor;
using UnityEngine;

/*
 * Projectile reflection demonstration in Unity 3D
 * 
 * Demonstrates a projectile reflecting in 3D space a variable number of times.
 * Reflections are calculated using Raycast's and Vector3.Reflect
 * 
 * Developed on World of Zero: https://youtu.be/GttdLYKEJAM
 */
public class ProjectileReflectionEmitterUnityNative : MonoBehaviour
{
    public int maxReflectionCount = 5;
    public float maxStepDistance = 1000;
    public GameObject line;
    public LineRenderer[] lr;
    int currentLaser = 0;
    void Start()
    {
        lr = new LineRenderer[maxReflectionCount];
        // lr = GetComponent<LineRenderer>();

        for (int i=0;i< maxReflectionCount; i++)
        {
            lr[i] = new GameObject().AddComponent<LineRenderer>();
            //lr[i] = line.GetComponent<LineRenderer>();
            lr[i].widthMultiplier = 0.1f;
            lr[i].gameObject.transform.SetParent(transform, false);
            // just to be sure reset position and rotation as well
            lr[i].gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

    }
#if  UNITY_EDITOR
    void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.25f);
       // lr.SetPosition(0, transform.position);
        DrawPredictedReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
    }
    // hard on trigers
    private void DrawPredictedReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {
       



        if (reflectionsRemaining == 0)
        {
            return;
        }


        lr[reflectionsRemaining-1].SetPosition(0, position);
        Vector3 startingPosition = position;

        Ray ray = new Ray(position, direction);

    //    RaycastHit hit;
     //   if (Physics.Raycast(position, direction, out hit))


            RaycastHit hit;
        // if (Physics.Raycast(ray, out hit, maxStepDistance))
        if (Physics.Raycast(ray, out hit))
        {
            if ((hit.collider) && (hit.collider.tag != "Trigger"))
            {

                lr[reflectionsRemaining - 1].SetPosition(1, hit.point);
                direction = Vector3.Reflect(direction, hit.normal);
                position = hit.point;

            }


            else
            {
                lr[reflectionsRemaining - 1].SetPosition(1, hit.point);
            //    direction = Vector3.Reflect(direction, hit.normal);
                position = hit.point;
            }
           // lr[reflectionsRemaining - 1].SetPosition(1, direction * 2000);

        }
        else
        {
            position += direction * maxStepDistance;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startingPosition, position);

        DrawPredictedReflectionPattern(position, direction, reflectionsRemaining - 1);
    }
#endif
}