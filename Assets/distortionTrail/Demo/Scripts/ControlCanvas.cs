/*
ControlCanvas.cs
just a script used to demo this asset
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlCanvas : MonoBehaviour 
{

#region Variables
    public Slider CameraRotX;
    public Slider CameraRotY;
    public Slider CameraRotZ;


    public Slider DisortionSlider;
    public Slider WidthSlider;


    public Transform CameraPivot;
    public Material thisMaterial;
    public TrailRenderer thisTrail;

    public Texture[] normalMaps;
    public int index = 0;
    private int _index;

#endregion

#region MonoMethods
    void Start()
    {
        thisMaterial.SetTexture("_NormalMap",normalMaps[_index]);
    }


	void Update () 
    {
        CameraPivot.rotation = Quaternion.Euler(CameraRotX.value,CameraRotY.value,CameraRotZ.value);

        thisMaterial.SetFloat("_Distortion",DisortionSlider.value);

        thisTrail.widthMultiplier = WidthSlider.value;
	}
#endregion 

#region normalMapMethods
    public void NMChange(int d)
    {
        index += d;

        _index = Mathf.Abs(index) % normalMaps.Length;

        thisMaterial.SetTexture("_NormalMap",normalMaps[_index]);
    }

    public void NMU()
    {
        NMChange(1);
    }

    public void NMD()
    {
        NMChange(-1);
    }
#endregion

}
