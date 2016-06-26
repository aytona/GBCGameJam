using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class GameLevelEditor : EditorWindow {

	private Texture modelTexture;
	private int numberOfModels = 1;
	private Vector2 MinVal;
	private Vector2 MaxVal;
	private GameObject[] modelsArray = new GameObject[1];

	[MenuItem("LevelEditor/CityGenerator")]
	public static void OpenLevelEditorWindow()
	{
        GetWindow<GameLevelEditor>(true, "City Generator");
	}

	void OnGUI()
	{
		MinVal = EditorGUILayout.Vector2Field("Min Values", MinVal);
		MaxVal = EditorGUILayout.Vector2Field("Max Values", MaxVal);

		GUILayout.Label("Number of Models", EditorStyles.boldLabel);
		numberOfModels = (int)GUILayout.HorizontalScrollbar(numberOfModels, 1.0f, 1.0f, 5.0f);

		GUILayout.Label("Model Texture", EditorStyles.boldLabel);
		modelTexture = EditorGUILayout.ObjectField(modelTexture, typeof(Texture), false) as Texture;

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
			if (MinVal.x == 0 || MinVal.y == 0)
			{
				ShowNotification(new GUIContent("Max numbers must be nonzero"));
				return;
			}
			if (MaxVal.x > MinVal.x || MaxVal.y > MinVal.y)
			{
				ShowNotification(new GUIContent("Min numbers must be smaller than Max numbers"));
				return;
			}
			if (modelTexture == null)
			{
				ShowNotification(new GUIContent("Texture must be filled"));
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

			GenerateLevel(MinVal.x, MaxVal.x, MinVal.y, MaxVal.y, modelsArray, modelTexture);
		}
	}

	private static void GenerateLevel(float xMin, float xMax, float yMin, float yMax, GameObject[] models, Texture modelTexts)
	{
		List<Vector3> spawnNodes = new List<Vector3>();
		GameObject rootObject = new GameObject();
		rootObject.name = "Level";
		Vector3 currentLocation = new Vector3(xMin, 0, yMin);
		float[] modelWidths = new float[models.Length];
		for (int i = 0; i < models.Length; i++)
		{
			modelWidths[i] = 
				(models[i].GetComponent<MeshFilter>().sharedMesh.bounds.size.x * 2.0f); // Multiply by 2 because its the X-length from the center
		}
		for (float nextWidth = 0; currentLocation.x + nextWidth <  xMax;)
		{
			int randomIndex = Random.Range(0, models.Length - 1);
			int nextIndex = Random.Range(0, models.Length - 1);
			nextIndex = lastIndexCheck(nextIndex, randomIndex) ? nextIndex : indexBorderCheck(nextIndex, models.Length) ? ++nextIndex : --nextIndex; // I don't even know
			spawnNodes.Add(new Vector3(currentLocation.x + nextWidth, 0, yMin));
			currentLocation.x += modelWidths[randomIndex];
			nextWidth = modelWidths[nextIndex];
		}
		foreach(Vector3 i in spawnNodes)
		{
			GameObject obj = new GameObject();
			obj.transform.SetParent(rootObject.transform);
			obj.transform.position = i;
			spawnNodes.Remove(i);
		}
	}

	private static bool lastIndexCheck(int next, int rand)
	{
		if (next != rand)
			return true;
		else
			return false;
	}

	private static bool indexBorderCheck(int next, int length)
	{
		if (next++ > length)
			return false;
		else
			return true;
	}
}
