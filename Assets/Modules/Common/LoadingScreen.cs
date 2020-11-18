﻿using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject screen;
    [SerializeField] private RectTransform loadingIcon;

    private void Awake()
    {
        gameObject.SetActive(true);
        screen.SetActive(false);
    }

    public void StartLoading()
    {
        screen.SetActive(true);
        LeanTween.rotateZ(loadingIcon.gameObject, 2000f, 1f)
            .setEaseInQuad()
            .setLoopPingPong(0);
    }

    public void StopLoading()
    {
        screen.SetActive(false);
    }
}