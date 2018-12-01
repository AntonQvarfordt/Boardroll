using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowTarget : MonoBehaviour
{
    public Transform Target;
    public float SmoothRate;
    public Vector3 Offset = new Vector3(1 , 0 , 0);
    private Vector3 _smoothRef;

    public void Init(Transform target, float smoothRate)
    {
        Target = target;
        SmoothRate = smoothRate;
    }

    private void FixedUpdate()
    {
        if (Target == null)
            return;

        var smoothPos = Vector3.SmoothDamp(transform.position , Target.position + Offset , ref _smoothRef , SmoothRate);
        transform.position = smoothPos;
    }
}
