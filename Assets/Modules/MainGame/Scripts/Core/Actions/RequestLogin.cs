using System;
using System.Threading.Tasks;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;
using Modules.MainGame.Scripts.Core.Domain;
using Modules.MainGame.Scripts.Infrastructure;
using Modules.MainGame.Scripts.Presentation;
using Modules.PlayerModule.Scripts.Core.Domain;
using Modules.PlayerModule.Scripts.Core.Domain.Repositories;
using Modules.Services;
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

        public IObservable<LoginResponse> Execute()
        {
            return gameGateway.RequestLogin()
                .Where(response => response.success)
                .SelectMany(response => InitServices(response).ToObservable())
                .Do(ProcessResponse);
        }

        private async Task<LoginResponse> InitServices(LoginResponse data)
        {
            await UnityServicesManager.Init();
            await UnityServicesManager.LoadData();
            return data;
        }

        private void ProcessResponse(LoginResponse response)
        {
          
            playerRepository.Save(new Player(response.playerId));
            Debug.Log("Saving on player repository");
            var actor = actorRepository.Get(response.playerId);
            if (actor.Value == null) return;
            sessionRepository.Save(new Session(response.playerId, actor.Value.id));
            
        }
    }

    public struct LoginResponse
    {
        public bool success;
        public string message;
        public string playerId;

        public LoginResponse(bool success, string message, string playerId)
        {
            this.success = success;
            this.message = message;
            this.playerId = playerId;
        }
    }
}