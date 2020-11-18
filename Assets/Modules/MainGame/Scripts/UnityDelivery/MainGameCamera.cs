﻿
using System;
using Modules.Common;
using UniRx;
using UnityEngine;

public class MainGameCamera : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera uiCamera;
    
    public IObservable<Unit> ShowMainGame()
    {
        var endRotation = new Vector3(20, 0, 0);
        return LeanTween.rotate(mainCamera.gameObject, endRotation, 1.5f)
            .setEaseInQuad()
            .OnCompleteAsObservable();
    }
    
}
