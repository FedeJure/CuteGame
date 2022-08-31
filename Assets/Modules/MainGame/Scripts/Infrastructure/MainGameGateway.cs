using System;
using Modules.MainGame.Scripts.Core.Actions;

namespace Modules.MainGame.Scripts.Infrastructure
{
    public interface MainGameGateway
    {
        IObservable<LoginResponse> RequestLogin();
    }
}