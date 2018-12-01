using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ColorPaletteAsset" , menuName = "Itatake/ColorPaletteAsset" , order = 0)]
public class ColorPalettePackage : ScriptableObject
{
	public Color BackgroundA;
	public Color BackgroundB;
	public Color BackgroundC;
	public Color AccentA;
	public Color AccentB;

	public List<Color> GetColors()
	{
		var colorList = new List<Color>();
		colorList.Add(BackgroundA);
		colorList.Add(BackgroundB);
		colorList.Add(BackgroundC);
		colorList.Add(AccentA);
		colorList.Add(AccentB);
		return colorList;
	}
}