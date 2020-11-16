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

        [SerializeField] GameObject loginScreen;
        [SerializeField] GameObject uiCamera;
        [SerializeField] Button loginButton;
        [SerializeField] TMP_InputField emailInput;
        [SerializeField] TMP_InputField passwordInput;
        
        [SerializeField] LoadingScreen loadingScreen;

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
            uiCamera.SetActive(false);
        }

        public void ShowFailedLoginFeedback(string message)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void StartMainGame()
        {
            throw new NotImplementedException();
        }
    }
}