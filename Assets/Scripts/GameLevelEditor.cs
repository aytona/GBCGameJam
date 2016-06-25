using UnityEngine;
using UnityEditor;
using System.Collections;

public class GameLevelEditor : EditorWindow {

	private int numberOfModels = 1;
	private GameObject[] modelsArray = new GameObject[1];

	[MenuItem("LevelEditor/CityGenerator")]
	public static void OpenLevelEditorWindow()
	{
		EditorWindow.GetWindow<GameLevelEditor>(true, "City Generator");
	}

	void OnGUI()
	{
		GUILayout.Label("Number of Models", EditorStyles.boldLabel);
		numberOfModels = (int)GUILayout.HorizontalScrollbar(numberOfModels, 1.0f, 1.0f, 20.0f);

		GUILayout.Label("Model Prefabs", EditorStyles.boldLabel);

		if (modelsArray.Length != numberOfModels)
		{
			modelsArray = new GameObject[numberOfModels];
		}
		for(int i = 0; i < modelsArray.Length; i++)
		{
			modelsArray[i] = EditorGUILayout.ObjectField(modelsArray[i], typeof(GameObject), true) as GameObject;
		}
		if (GUILayout.Button("Generate"))
		{
			if (numberOfModels == 0)
			{
				ShowNotification (new GUIContent("Number of models must be nonzero"));
				return;
			}
			foreach(GameObject i in modelsArray)
			{
				if (i == null)
				{
					ShowNotification (new GUIContent("Each array must be filled"));
					return;
				}
			}
		}
	}
}
