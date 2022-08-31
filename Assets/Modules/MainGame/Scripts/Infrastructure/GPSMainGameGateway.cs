using System;
using Modules.MainGame.Scripts.Core.Actions;
using Modules.Services;
using UniRx;

namespace Modules.MainGame.Scripts.Infrastructure
{
    public class GPSMainGameGateway: MainGameGateway
    {
        public IObservable<LoginResponse> RequestLogin()
        {
            return GooglePlayServicesManager.Login().Select((success) =>
            {
                var localUser = GooglePlayServicesManager.GetLocalUser();
                return new LoginResponse(success, success ? "Login Successful" : "Login Failed", localUser.id);
            });
        }
    }
}