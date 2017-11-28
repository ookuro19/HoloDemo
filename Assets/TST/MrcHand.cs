using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;



public class MrcHand : MonoBehaviour {

    [Tooltip("unknow for null")]
    public InteractionSourceHandedness Handness;

    public GameObject Socre;

    private InteractionSourceState SourceState;

    Vector3 pos;
    Quaternion rot;
	
	// Update is called once per frame
	void Update () {

        if (Handness == InteractionSourceHandedness.Unknown)
        {
            return;
        }

        foreach (var sourceState in InteractionManager.GetCurrentReading())
        {
            if (sourceState.source.handedness == Handness)
            {
                if (sourceState.sourcePose.TryGetPosition(out pos))
                {
                      transform.localPosition = pos;
                }
                   
                if (sourceState.sourcePose.TryGetRotation(out rot))
                {
                    transform.localRotation = rot;
                }

               //// Debug.Log("headmove  +  " + HeadMove.triggerNums);
               // if (sourceState.selectPressed  || Input.GetMouseButton(0))
               // {
               //     // Debug.Log("Jump");
               //     if (Handness == InteractionSourceHandedness.Left)
               //     {
               //         HeadMove.lefttrigger = true;
               //     }
               //     if (Handness == InteractionSourceHandedness.Right)
               //     {
               //         HeadMove.righttrigger = true;
               //     }
               // }
               // else
               // {
               //     if (Handness == InteractionSourceHandedness.Left)
               //     {
               //         HeadMove.lefttrigger = false;
               //     }
               //     if (Handness == InteractionSourceHandedness.Right)
               //     {
               //         HeadMove.righttrigger = false;
               //     }
               // }
                if (sourceState.touchpadPressed)
                {
                    Socre.SetActive(true);
                }
                else
                {
                    Socre.SetActive(false);
                }
            }
            else
            {
                Socre.SetActive(false);
            }
        }




    }
}
