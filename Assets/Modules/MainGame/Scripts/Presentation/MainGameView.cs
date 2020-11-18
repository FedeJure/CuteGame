using System;
using Modules.ActorModule.Scripts.UnityDelivery.Skin;

namespace Modules.MainGame.Scripts.Presentation
{
    public interface MainGameView
    {
        event Action OnViewEnable;
        event Action OnViewDisable;
        event Action<LoginData> OnLoginClicked;
        event Action<CreationData> OnCreationCompleted;
        void ShowLoginScreen();
        void ShowActorCreationScreen();
        void StartMainGame();
        void InitView();
        void ShowFailedLoginFeedback(string message);
        void ShowSuccessLoginFeedback();
        void ShowLoading();
        void HideLoading();
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
        public ActorSkinData bodySkin;
        public ActorSkinData headSkin;

        public CreationData(string name, ActorSkinData bodySkin, ActorSkinData headSkin)
        {
            this.name = name;
            this.bodySkin = bodySkin;
            this.headSkin = headSkin;
        }
    }
}