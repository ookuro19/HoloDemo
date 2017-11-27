using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TSTFocusEvent : MonoBehaviour {

    public enum SphereName
    {
        LeftSphere = 1,
        RightSphere = 2
    }
    private SphereName curSphereName;

    private Text tstText;

    void Start()
    {
        tstText = GetComponent<Text>();
    }

    public void FocusObjectName(int i)
    {
        if (tstText != null)
        {
            if (i == 1)
            {
                tstText.text = "LeftSphere";
            }
            else if (i == 2)
            {
                tstText.text = "RightSphere";
            }
            else
            {
                tstText.text = " ";
            }
        }
    }
}
