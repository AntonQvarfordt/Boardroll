using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(VisualizationBrain))]
public class VisualizationBrainEditor : Editor
{

	public override void OnInspectorGUI()
	{
		var targetScript = (VisualizationBrain)target;

		GUILayout.BeginHorizontal();
		GUILayout.Box("low");
		GUILayout.Box("mid");
		GUILayout.Box("high");
		GUILayout.EndHorizontal();

		var volume = 0f;

		if (targetScript.IsSongLoaded)
			volume = targetScript.GetVolume;

		GUILayout.HorizontalSlider(volume , 0 , 80);

		base.OnInspectorGUI();
	}

}
