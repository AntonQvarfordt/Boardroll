using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColorPalettePackage))]
public class ColorPalettePackageEditor : Editor
{

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		var targetScript = (ColorPalettePackage)target;

		if (GUILayout.Button("Set Active"))
		{
			var dataContainer = new SaveData();
			dataContainer.fileName = "ColorPalette";
			dataContainer["ColorPalette"] = targetScript.GetColors();
			dataContainer.Save();
		}
	}
}