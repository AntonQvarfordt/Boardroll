using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public struct ColorGradient
{
	public Color Primary;

	public Color C1;
	public Color C2;
	public Color C3;
	public Color C4;
	public Color C5;
	public Color C6;
	public Color C7;
	public Color C8;
	public Color C9;
	public Color C10;
}

[CreateAssetMenu(fileName = "ColorPaletteAsset" , menuName = "Itatake/ColorPaletteAsset" , order = 0)]
public class ColorPalettePackage : ScriptableObject
{
	public ColorGradient PrimaryColor;
	public ColorGradient SecondaryColor;
}