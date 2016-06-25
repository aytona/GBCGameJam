using UnityEngine;
using UnityEditor;
using System.Collections;

public class GameLevelEditor : EditorWindow {

	private int numberOfModels = 1;
	private int minModels;
	private int maxModels;
	private GameObject[] modelsArray = new GameObject[1];

	[MenuItem("LevelEditor/CityGenerator")]
	public static void OpenLevelEditorWindow()
	{
		EditorWindow.GetWindow<GameLevelEditor>(true, "City Generator");
	}

	void OnGUI()
	{
		GUILayout.Label("Min Models", EditorStyles.boldLabel);
		minModels = EditorGUILayout.IntField(minModels, GUILayout.Width(30));

		GUILayout.Label("MaxModels", EditorStyles.boldLabel);
		maxModels = EditorGUILayout.IntField(maxModels, GUILayout.Width(30));

		GUILayout.Label("Number of Models", EditorStyles.boldLabel);
		numberOfModels = (int)GUILayout.HorizontalScrollbar(numberOfModels, 1.0f, 1.0f, 5.0f);

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
			if (minModels == 0 || maxModels == 0)
			{
				ShowNotification(new GUIContent("Number must be nonzero"));
				return;
			}
			if (minModels > maxModels)
			{
				ShowNotification(new GUIContent("Min Models must be smaller than Max Models"));
				return;
			}
			foreach(GameObject i in modelsArray)
			{
				if (i == null)
				{
					ShowNotification (new GUIContent("Each array must be filled"));
					return;
				}
				GenerateLevel(minModels, maxModels, modelsArray);
			}
		}
	}

	private static void GenerateLevel(int min, int max, GameObject[] models)
	{

	}
}
