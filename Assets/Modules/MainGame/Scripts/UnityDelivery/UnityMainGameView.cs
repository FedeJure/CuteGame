using System;
using Modules.MainGame.Scripts.Presentation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.MainGame.Scripts.UnityDelivery
{
    public class UnityMainGameView: MonoBehaviour, MainGameView
    {
        public event Action OnViewEnable = () => {};
        public event Action OnViewDisable = () => {};
        public event Action<LoginData> OnLoginClicked = data => {};

        [SerializeField] Button loginButton;
        [SerializeField] TMP_InputField emailInput;
        [SerializeField] TMP_InputField passwordInput;
        [SerializeField] MainGameCamera gameCamera;

        [SerializeField] LoadingScreen loadingScreen;
        [SerializeField] GameObject loginScreen;
        [SerializeField] GameObject gui;

        private void Awake()
        {
            MainGameModuleProvider.ProvidePresenterFor(this);

            loginButton.onClick.AddListener(SetOnLoginClicked);
        }

        private void SetOnLoginClicked()
        {
            OnLoginClicked(new LoginData(emailInput.text, passwordInput.text));
        }

        private void OnEnable()
        {
            OnViewEnable();
        }

        private void OnDisable()
        {
            OnViewDisable();
        }

        public void InitView()
        {
            gui.SetActive(false);
            loginScreen.SetActive(false);
        }

        public void ShowFailedLoginFeedback(string message)
        {
            Debug.LogWarning("Failed successful");
        }

        public void ShowSuccessLoginFeedback()
        {
            Debug.LogWarning("Loging successful");
        }

        public void ShowLoading()
        {
            loadingScreen.StartLoading();
        }

        public void HideLoading()
        {
            loadingScreen.StopLoading();
        }

        public void ShowLoginScreen()
        {
            loginScreen.SetActive(true);
        }

        public void ShowActorCreationScreen()
        {
            Debug.LogWarning("Show actor creation screen");
        }

        public void StartMainGame()
        {
            Debug.LogWarning("Start main game");
            loginScreen.SetActive(false);
            gameCamera.ShowMainGame();
            gui.SetActive(true);
            
        }
    }
}