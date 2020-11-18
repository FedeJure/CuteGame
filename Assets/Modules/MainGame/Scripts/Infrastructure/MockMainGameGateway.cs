using System;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.Common;
using Modules.MainGame.Scripts.Core.Actions;
using Modules.PlayerModule.Scripts.Core.Domain;
using UniRx;

namespace Modules.MainGame.Scripts.Infrastructure
{
    public class MockMainGameGateway : MainGameGateway
    {
        public IObservable<LoginResponse> RequestLogin(string email, string password)
        {
            var player = new Player(1);
            return new LoginResponse(true, "Mock success message",player, null).ToObservableDummy()
                .Delay(TimeSpan.FromSeconds(3));
        }
    }
}