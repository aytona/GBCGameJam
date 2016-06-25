using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MainMenuUI : MonoBehaviour {

	public GameObject[] screenObjs;

	public void Play()
	{
		Application.LoadLevel("Game");
	}

	public void ScreenTransition()
	{
		string name = EventSystem.current.currentSelectedGameObject.tag;
		ScreenChange(name);
	}

	public void Exit()
	{
		Application.Quit();
	}

	private void ScreenChange(string name)
	{
		foreach(GameObject i in screenObjs)
		{
			if (i.name != name)
			{
				i.SetActive(false);
			}
			else
			{
				i.SetActive(true);
			}
		}
	}
}
