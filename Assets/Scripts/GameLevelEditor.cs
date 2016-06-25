using UnityEngine;
using UnityEditor;
using System.Collections;

public class GameLevelEditor : EditorWindow {

	private int numberOfModels = 1;
	private Vector2 Xminmax;
	private Vector2 Yminmax;
	private GameObject[] modelsArray = new GameObject[1];

	[MenuItem("LevelEditor/CityGenerator")]
	public static void OpenLevelEditorWindow()
	{
		EditorWindow.GetWindow<GameLevelEditor>(true, "City Generator");
	}

	void OnGUI()
	{
		Xminmax = EditorGUILayout.Vector2Field("X min and max", Xminmax);
		Yminmax = EditorGUILayout.Vector2Field("Y min and max", Yminmax);

		GUILayout.Label("Number of Models", EditorStyles.boldLabel);
		numberOfModels = (int)GUILayout.HorizontalScrollbar(numberOfModels, 1.0f, 1.0f, 5.0f);

		GUILayout.Label("Model Prefabs", EditorStyles.boldLabel);
		if (modelsArray.Length != numberOfModels)
		{
			modelsArray = new GameObject[numberOfModels];
		}
		for(int i = 0; i < modelsArray.Length; i++)
		{
			modelsArray[i] = EditorGUILayout.ObjectField(modelsArray[i], typeof(GameObject), false) as GameObject;
		}
		if (GUILayout.Button("Generate"))
		{
			if (Xminmax.y == 0 || Yminmax.y == 0)
			{
				ShowNotification(new GUIContent("Max numbers must be nonzero"));
				return;
			}
			if (Xminmax.x > Xminmax.y || Yminmax.x > Yminmax.y)
			{
				ShowNotification(new GUIContent("Min numbers must be smaller than Max numbers"));
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

			GenerateLevel(Xminmax.x, Xminmax.y, Yminmax.x, Yminmax.y, modelsArray);
		}
	}

	private static void GenerateLevel(float xMin, float xMax, float yMin, float yMax, GameObject[] models)
	{
		GameObject rootObject = new GameObject();
		rootObject.name = "Level";
		//Vector3 currentLocation = new Vector3(xMin, 0, yMin);
		float[] modelWidths = new float[models.Length];
		for (int i = 0; i < models.Length; i++)
		{
			modelWidths[i] = 
				(models[i].GetComponent<MeshFilter>().mesh.bounds.size.x * 2.0f); // Multiply by 2 because its the X-length from the center
		}
	}
}
