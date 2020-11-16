using System;

namespace Modules.MainGame.Scripts.Presentation
{
    public interface MainGameView
    {
        event Action OnViewEnable;
        event Action OnViewDisable;
        event Action<LoginData> OnLoginClicked;
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
}