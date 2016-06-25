using UnityEngine;
using System.Collections;

public class PlayerInteractions : MonoBehaviour {

	public GameObject buttonPrompt;

	private bool nearInteractable;

	void Update()
	{
		if (nearInteractable)
		{
			buttonPrompt.SetActive(true);
		}
		else if (!nearInteractable)
		{
			buttonPrompt.SetActive(false);
		}
	}

	#region Triggers
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Interactable")
		{
			nearInteractable = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Interactable")
		{
			nearInteractable = false;
		}
	}
	#endregion
}
