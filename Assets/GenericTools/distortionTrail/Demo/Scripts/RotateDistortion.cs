/*
Rotate.cs
Rotates the gameObject around 
*/


using UnityEngine;
using System.Collections;

public class RotateDistortion : MonoBehaviour {

#region Variables

    public bool xRotate;
    public bool yRotate;
    public bool zRotate;

    public float xSpeed;
    public float ySpeed;
    public float zSpeed;

    private float xRotation;
    private float yRotation;
    private float zRotation;
#endregion

#region MonoMethods
	// Update is called once per frame
	void FixedUpdate () 
    {

        if (xRotate)
        {
            xRotation += xSpeed;
        }
        if (yRotate)
        {
            yRotation += ySpeed;
        }
        if (zRotate)
        {
            zRotation += zSpeed;
        }


        gameObject.transform.rotation = Quaternion.Euler(xRotation,yRotation,zRotation);
	}
#endregion

}
