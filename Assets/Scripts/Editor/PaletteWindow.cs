using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PaletteWindow : EditorWindow
{
	private Vector2 _colorBoxSize = new Vector2(64f , 64f);
	private float _paddingA = 8;

	private static List<Color> _colors = new List<Color>();
	private static List<Texture2D> _coloredTextures = new List<Texture2D>();

	public List<ColorPalettePackage> ColorPackages = new List<ColorPalettePackage>();

	public static void SetColors( List<Color> colors )
	{
		_colors = colors;
	}

	[MenuItem("Window/Itatake/Style Window")]
	static void Init()
	{
		var savedData = SaveData.Load(Application.streamingAssetsPath + "\\" + "ColorPalette.uml");
		savedData.TryGetValue<List<Color>>("ColorPalette" , out _colors);

		PaletteWindow window = (PaletteWindow)EditorWindow.GetWindow(typeof(PaletteWindow));

		window.minSize = new Vector2(320 , 74);
		window.maxSize = new Vector2(320 , 74);

		window.Show();
	}

	private void Awake()
	{

		if (_colors.Count == 0)
			return;

		_coloredTextures.Clear();

		foreach (Color clr in _colors)
		{
			var texture = new Texture2D((int)_colorBoxSize.x , (int)_colorBoxSize.y);
			texture.EncodeToPNG();

			var fillColorArray = texture.GetPixels();

			for (var i = 0; i < fillColorArray.Length; ++i)
			{
				fillColorArray[i] = clr;
			}


			texture.SetPixels(fillColorArray);

			texture.Apply();
			_coloredTextures.Add(texture);
		}
	}

	void OnGUI()
	{

		if (_colors.Count == 0)
		{
			EditorGUILayout.LabelField("PaletteWindow Requires a StyleManager singleton");
			return;
		}
		for (int i = 0; i < _colors.Count; i++)
		{
			var color = _colors[i];
			ColorMenu(i , color);
		}
	}

	void ColorMenu( int index , Color color )
	{
		var rect = new Rect(_colorBoxSize.x * index , _paddingA , _colorBoxSize.x , _colorBoxSize.y);
		if (GUI.Button(rect , _coloredTextures[index]))
		{
			EditorGUIUtility.systemCopyBuffer = ColorUtility.ToHtmlStringRGBA(color);
		}
	}
}