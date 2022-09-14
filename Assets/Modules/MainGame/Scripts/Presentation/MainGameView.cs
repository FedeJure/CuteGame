using System;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.UnityDelivery.Skin;
using UniRx;

namespace Modules.MainGame.Scripts.Presentation
{
    public interface MainGameView
    {
        event Action OnViewEnable;
        event Action OnViewDisable;
        event Action OnLoginClicked;
        event Action<CreationData> OnCreationCompleted;
        void ShowLoginScreen();
        void ShowActorCreationScreen();
        void StartMainGame();
        void InitView();
        void ShowFailedLoginFeedback(string message);
        void ShowSuccessLoginFeedback();
        void ShowLoading();
        void HideLoading();
        IObservable<Unit> MoveCameraToCreationView();
        IObservable<Unit> MoveCameraToMainGame();
    }

    public struct LoginData
    {
        public string email;
        public string password;

        public LoginData(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }

    public struct CreationData
    {
        public string name;
        public Skin bodySkin;
        public Skin headSkin;

        public CreationData(string name, Skin bodySkin, Skin headSkin)
        {
            this.name = name;
            this.bodySkin = bodySkin;
            this.headSkin = headSkin;
        }
    }
}