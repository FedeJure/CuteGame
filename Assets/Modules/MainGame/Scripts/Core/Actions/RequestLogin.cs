using System;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;
using Modules.MainGame.Scripts.Core.Domain;
using Modules.MainGame.Scripts.Infrastructure;
using Modules.MainGame.Scripts.Presentation;
using Modules.PlayerModule.Scripts.Core.Domain;
using Modules.PlayerModule.Scripts.Core.Domain.Repositories;
using UniRx;
using UnityEngine;

namespace Modules.MainGame.Scripts.Core.Actions
{
    public class RequestLogin
    {
        private readonly MainGameGateway gameGateway;
        private readonly PlayerRepository playerRepository;
        private readonly ActorRepository actorRepository;
        private readonly SessionRepository sessionRepository;

        public RequestLogin() { }

        public RequestLogin(MainGameGateway gameGateway,
            PlayerRepository playerRepository,
            ActorRepository actorRepository,
            SessionRepository sessionRepository)
        {
            this.gameGateway = gameGateway;
            this.playerRepository = playerRepository;
            this.actorRepository = actorRepository;
            this.sessionRepository = sessionRepository;
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
            if (response.actor == null) return;
            actorRepository.Save(response.actor);
            sessionRepository.Save(new Session(response.player.id, response.actor.id));
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