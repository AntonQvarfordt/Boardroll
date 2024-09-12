using UnityEngine;

public class PositionPin : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "pin2.png", false);
        var ray = new Ray(transform.position, transform.forward);
        Gizmos.DrawRay(ray);
        //var transformF = transform.;
        //Debug.Log(transformF);
        //Gizmos.DrawLine(transform.position, transformF);

    }
}
