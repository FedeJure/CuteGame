
using System;
using Modules.Common;
using UniRx;
using UnityEngine;

public class MainGameCamera : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera uiCamera;

    private void Awake()
    {
        CameraRepository.SetGameCurrentCamera(mainCamera);
    }

    public IObservable<Unit> ShowCreationView()
    {
        var endRotation = new Vector3(3, -15, 0);
        return LeanTween.rotate(mainCamera.gameObject, endRotation, 1.5f)
            .setEaseInQuad()
            .OnCompleteAsObservable();
    }

    public IObservable<Unit> ShowMainGame()
    {
        var endRotation = new Vector3(0, 0, 0);
        return LeanTween.rotate(mainCamera.gameObject, endRotation, 1)
            .setEaseInQuad()
            .OnCompleteAsObservable();
    }
}
