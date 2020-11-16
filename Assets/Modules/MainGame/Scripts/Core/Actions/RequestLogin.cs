using System;
using Modules.MainGame.Scripts.Infrastructure;
using Modules.MainGame.Scripts.Presentation;

namespace Modules.MainGame.Scripts.Core.Actions
{
    public class RequestLogin
    {
        private readonly MainGameGateway gameGateway;
        
        public RequestLogin() { }

        public RequestLogin(MainGameGateway gameGateway)
        {
            this.gameGateway = gameGateway;
        }

        public virtual IObservable<LoginResponse> Execute(LoginData data)
        {
            return gameGateway.RequestLogin(data.email, data.password);
        }
    }

    public struct LoginResponse
    {
        public bool success;
        public string message;

        public LoginResponse(bool success, string message)
        {
            this.success = success;
            this.message = message;
        }
    }
}