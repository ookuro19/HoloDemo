using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using HoloToolkit.Unity.InputModule;

public class MeasureManagerTST : MonoBehaviour, IInputClickHandler
{
	public GameObject tstCube;

	private void Start()
	{
		InputManager.Instance.PushFallbackInputHandler(gameObject);
	}

	public void OnInputClicked(InputClickedEventData eventData)
	{
		if (eventData.selectedObject)
		{
			Debug.Log(eventData.selectedObject.name);

		}
		else
		{
			Debug.Log("selectedObject is null");
			tstCube.SetActive(!tstCube.activeSelf);
		}
	}
}