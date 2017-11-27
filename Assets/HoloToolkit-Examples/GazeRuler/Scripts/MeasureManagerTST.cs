// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;
using System.Collections.Generic;
using System;
using HoloToolkit.Unity.InputModule;

namespace HoloToolkit.Examples.GazeRuler
{
    /// <summary>
    /// manager all measure tools here
    /// </summary>
    public class MeasureManagerTST : MonoBehaviour, IInputClickHandler
    {
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
            }
        }
    }
}
