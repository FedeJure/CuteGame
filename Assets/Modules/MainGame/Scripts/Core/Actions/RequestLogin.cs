using System;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.MainGame.Scripts.Infrastructure;
using Modules.MainGame.Scripts.Presentation;
using Modules.PlayerModule.Scripts.Core.Domain;
using Modules.PlayerModule.Scripts.Core.Domain.Repositories;
using UniRx;

namespace Modules.MainGame.Scripts.Core.Actions
{
    public class RequestLogin
    {
        private readonly MainGameGateway gameGateway;
        private readonly PlayerRepository playerRepository;
        private readonly ActorRepository actorRepository;

        public RequestLogin() { }

        public RequestLogin(MainGameGateway gameGateway,
            PlayerRepository playerRepository,
            ActorRepository actorRepository)
        {
            this.gameGateway = gameGateway;
            this.playerRepository = playerRepository;
            this.actorRepository = actorRepository;
        }

        public virtual IObservable<LoginResponse> Execute(LoginData data)
        {
            return gameGateway.RequestLogin(data.email, data.password)
                .Do(ProcessResponse);
        }

        private void ProcessResponse(LoginResponse response)
        {
            if (!response.success) return;
            playerRepository.Save(response.player);
            if (response.actor != null)
            {
                actorRepository.Save(response.actor);
            }
        }
    }

    public struct LoginResponse
    {
        public bool success;
        public string message;
        public Player player;
        public Actor actor;

        public LoginResponse(bool success, string message, Player player, Actor actor)
        {
            this.success = success;
            this.message = message;
            this.player = player;
            this.actor = actor;
        }
    }
}