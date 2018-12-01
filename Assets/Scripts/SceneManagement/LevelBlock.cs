using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlock : MonoBehaviour {

	public float GetXPos
	{
		get
		{
			return transform.position.x;
		}
	}

	public float SceneModuleWidth = 12;
}
