﻿using System;
using Modules.MainGame.Scripts.Presentation;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.MainGame.Scripts.UnityDelivery
{
    public class UnityMainGameView: MonoBehaviour, MainGameView
    {
        public event Action OnViewEnable = () => {};
        public event Action OnViewDisable = () => {};
        public event Action OnLoginClicked = () => {};
        public event Action<CreationData> OnCreationCompleted = data => { };

        [SerializeField] Button loginButton;
        [SerializeField] TMP_InputField emailInput;
        [SerializeField] TMP_InputField passwordInput;
        [SerializeField] MainGameCamera gameCamera;

        [SerializeField] LoadingScreen loadingScreen;
        [SerializeField] GameObject loginScreen;
        [SerializeField] GameObject gui;

        [SerializeField] UnityActorCreationView creationView;
        
        private void Awake()
        {
            InitView();
            MainGameModuleProvider.ProvidePresenterFor(this);

            loginButton.onClick.AddListener(SetOnLoginClicked);
            creationView.OnCreate += OnCreationCompleted;
        }
        

        public void InitView()
        {
            gui.SetActive(false);
            loginScreen.SetActive(false);
            creationView.gameObject.SetActive(false);
        }

        private void SetOnLoginClicked()
        {
            OnLoginClicked();
        }

        private void OnEnable()
        {
            OnViewEnable();
        }

        private void OnDisable()
        {
            OnViewDisable();
        }

        public void ShowFailedLoginFeedback(string message)
        {
            Debug.Log("Failed successful");
        }

        public void ShowSuccessLoginFeedback()
        {
            Debug.Log("Login successful");
        }

        public void ShowLoading()
        {
            loadingScreen.StartLoading();
        }

        public void HideLoading()
        {
            loadingScreen.StopLoading();
        }

        public IObservable<Unit> MoveCameraToCreationView()
        {
            return gameCamera.ShowCreationView();
        }

        public IObservable<Unit> MoveCameraToMainGame()
        {
            return gameCamera.ShowMainGame();
        }

        public void ShowLoginScreen()
        {
            loginScreen.SetActive(true);
        }

        public void ShowActorCreationScreen()
        {
            creationView.gameObject.SetActive(true);
        }

        public void StartMainGame()
        {
            loginScreen.SetActive(false);
            creationView.gameObject.SetActive(false);
            gui.SetActive(true);
        }
    }
}