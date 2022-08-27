using System;
using Modules.MainGame.Scripts.Core.Actions;
using Modules.PlayerModule.Scripts.Core.Domain;
using UniRx;

namespace Modules.MainGame.Scripts.Infrastructure
{
    public class GPSMainGameGateway: MainGameGateway
    {
        public IObservable<LoginResponse> RequestLogin(string email, string password)
        {
            return GooglePlayServicesManager.Login().Select((success) =>
            {
                var localUser = GooglePlayServicesManager.GetLocalUser();
                var player = new Player(localUser.id);
                return new LoginResponse(success, success ? "Login Successful" : "Login Failed", player, null);
            });
        }
    }
}