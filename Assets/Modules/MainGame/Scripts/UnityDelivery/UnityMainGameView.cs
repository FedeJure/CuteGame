using System;
using Modules.MainGame.Scripts.Presentation;
using UnityEngine;

namespace Modules.MainGame.Scripts.UnityDelivery
{
    public class UnityMainGameView: MonoBehaviour, MainGameView
    {
        public event Action OnViewEnable = () => {};
        public event Action OnViewDisable = () => {};
        public event Action<LoginData> OnLoginClicked;

        [SerializeField] GameObject loginScreen;
        [SerializeField] private GameObject uiCamera;

        private void Awake()
        {
            MainGameModuleProvider.ProvidePresenterFor(this);
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