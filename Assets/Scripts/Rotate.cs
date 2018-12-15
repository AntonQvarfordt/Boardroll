using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public int speed = 100;

    // Use this for initialization
    void Update () 
	{
		transform.Rotate (speed * Time.deltaTime, 0, 0);
	}
}
