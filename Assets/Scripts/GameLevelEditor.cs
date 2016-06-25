using UnityEngine;
using UnityEditor;
using System.Collections;

public class GameLevelEditor : EditorWindow {

	private int numberOfModels;

	[MenuItem("LevelEditor/CityGenerator")]
	public static void OpenLevelEditorWindow()
	{
		EditorWindow.GetWindow<GameLevelEditor>(true, "City Generator");
	}

	void OnGUI()
	{
		GUILayout.Label("Number of Models", EditorStyles.boldLabel);
		numberOfModels = (int)GUILayout.HorizontalScrollbar(numberOfModels, 1.0f, 0.0f, 20.0f);

		GUILayout.Label("Model Prefabs", EditorStyles.boldLabel);
		GameObject[] modelsArray = new GameObject[numberOfModels];
		foreach(GameObject i in modelsArray)
		{
			EditorGUILayout.ObjectField(i, typeof(GameObject), true);
		}

		if (GUILayout.Button("Generate"))
		{

		}
	}
}
