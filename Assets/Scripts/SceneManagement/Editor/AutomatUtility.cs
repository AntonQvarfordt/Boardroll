using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AutomatUtility : MonoBehaviour {
	[MenuItem("Tools/Automat/Create GameManager")]
	public static void CreateGameManager()
	{
		var go = new GameObject();
		go.name = "GameManager";
		go.AddComponent<GameManager>();
	}

	[MenuItem("Tools/Automat/Create Headers")]
	public static void CreateHeaders()
	{
		var go = new GameObject();
		go.name = "--SCENE--";
		var go2 = new GameObject();
		go2.name = "--MANAGE--";
		var go3 = new GameObject();
		go3.name = "--WORLD--";
		var go4 = new GameObject();
		go4.name = "--PLAY--";

	}
}
