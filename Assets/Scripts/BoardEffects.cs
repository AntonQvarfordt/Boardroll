using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardEffects : MonoBehaviour {

    public BoardState BoardStateScript;
    public float BaseRotateSpeed = 1000;
    public Rotate[] WheelRotate;

    private void Update()
    {
        foreach (Rotate rot in WheelRotate)
        {
            var rSp = BoardStateScript.GetVelocity.x;
            rot.speed = (int)BaseRotateSpeed * (int)rSp;
        }
    }

}
