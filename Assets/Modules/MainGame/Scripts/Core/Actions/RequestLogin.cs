﻿using System;
using System.Threading.Tasks;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Action;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;
using Modules.MainGame.Scripts.Core.Domain;
using Modules.MainGame.Scripts.Infrastructure;
using Modules.PlayerModule.Scripts.Core.Domain;
using Modules.PlayerModule.Scripts.Core.Domain.Repositories;
using Modules.Services;
using UniRx;

namespace Modules.MainGame.Scripts.Core.Actions
{
    public class RequestLogin
    {
        private readonly MainGameGateway gameGateway;
        private readonly PlayerRepository playerRepository;
        private readonly SessionRepository sessionRepository;
        private readonly RetrieveActor retrieveActor;

        public RequestLogin() { }

        public RequestLogin(MainGameGateway gameGateway,
            PlayerRepository playerRepository,
            SessionRepository sessionRepository,
            RetrieveActor retrieveActor)
        {
            this.gameGateway = gameGateway;
            this.playerRepository = playerRepository;
            this.sessionRepository = sessionRepository;
            this.retrieveActor = retrieveActor;
        }

        public virtual IObservable<LoginResponse> Execute()
        {
            #if UNITY_EDITOR
            return Observable.Return(new LoginResponse(true, "Success", "playerId"))
                .SelectMany(ProcessResponse);
            #endif
            return gameGateway.RequestLogin()
                .Where(response => response.success)
                .SelectMany(InitServices)
                .SelectMany(ProcessResponse);
        }

        private IObservable<LoginResponse> InitServices(LoginResponse data)
        {
            return UnityServicesManager.Init().ToObservable()
                .SelectMany(_ => UnityServicesManager.LoadData().ToObservable())
                .Select(_ => data);
        }

        private IObservable<LoginResponse> ProcessResponse(LoginResponse response)
        {
          
            playerRepository.Save(new Player(response.playerId));
            return retrieveActor.Execute()
                .Do(actor =>
                {
                    if (actor.hasValue) sessionRepository.Save(new Session(response.playerId, actor.Value.id));
                })
                .Select(_ => response);
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