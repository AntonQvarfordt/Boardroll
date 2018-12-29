using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardState))]
public class BoardStateEditor : Editor {
	public override void OnInspectorGUI()
	{
		var targetScript = (BoardState)target;

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Velocity: ");
		EditorGUILayout.LabelField(targetScript.GetVelocity.ToString());
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Is Grounded");
		EditorGUILayout.Toggle(targetScript.IsGrounded);
		EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Catch Window");
        EditorGUILayout.Toggle(targetScript.InCatchWindow);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Incapacitated");
        EditorGUILayout.Toggle(targetScript.IsIncapacitated);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        var activeStates = targetScript.ActiveStates;

		foreach (BoardStates state in activeStates)
		{
			GUILayout.Label(state.ToString());
		}

		base.OnInspectorGUI();
	}
}
