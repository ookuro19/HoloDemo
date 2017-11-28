using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.XR.WSA.Input;

public class TstInput : MonoBehaviour , IInputHandler ,IInputClickHandler
{
    public GameObject tstSphere;
    private GestureRecognizer mGestureRecognizer;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInputDown(InputEventData eventData)
    {
        Debug.Log("OnInputDown");
    }

    public void OnInputUp(InputEventData eventData)
    {
        Debug.Log("OnInputUp");
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("on input clicked");
        tstSphere.SetActive(!tstSphere.activeSelf);
    }
}
