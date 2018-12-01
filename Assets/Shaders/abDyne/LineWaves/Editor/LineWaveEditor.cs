using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// Line Wave v1.7
/// Use with Unity's LineRenderer
/// by Adriano Bini.
/// </summary>

[CustomEditor(typeof(LineWave))]
[CanEditMultipleObjects]
public class LineWaveEditor : Editor {

	public override void OnInspectorGUI ()
	{
//		LineWave myLineWave = (LineWave)target;
//		//TODO tooltips
//		GUIContent altRotationContent = new GUIContent("Alt Rotation", "Rotates wave when 'Target' is assigned.");
//		myLineWave.altRotation = EditorGUILayout.FloatField(altRotationContent, myLineWave.altRotation);

		DrawDefaultInspector();
		EditorGUILayout.HelpBox("'Alt Rotation' rotates wave when 'Target' is assigned.", MessageType.Info);

	}
}
