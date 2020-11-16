using System;
using Modules.Common;
using Modules.MainGame.Scripts.Core.Actions;

namespace Modules.MainGame.Scripts.Infrastructure
{
    public class MockMainGameGateway : MainGameGateway
    {
        public IObservable<LoginResponse> RequestLogin(string email, string password)
        {
            return new LoginResponse(true, "Mock success message").ToObservableDummy();
        }
    }
}