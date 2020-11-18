﻿using System;
using System.Collections.Generic;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;
using Modules.MainGame.Scripts.Core.Actions;
using Modules.PlayerModule.Scripts.Core.Domain.Repositories;
using UniRx;
using UnityEngine;

namespace Modules.MainGame.Scripts.Presentation
{
    public class MainGamePresenter
    {
        private readonly MainGameView view;
        private readonly PlayerRepository playerRepository;
        private readonly ActorRepository actorRepository;
        private readonly RequestLogin requestLogin;
        
        List<IDisposable> loginDisposer = new List<IDisposable>();

        public MainGamePresenter(MainGameView view,
            PlayerRepository playerRepository,
            ActorRepository actorRepository,
            RequestLogin requestLogin)
        {
            this.view = view;
            this.playerRepository = playerRepository;
            this.actorRepository = actorRepository;
            this.requestLogin = requestLogin;

            view.OnViewEnable += PresentView;
            view.OnViewDisable += DisposeView;
            view.OnLoginClicked += ProcessLogin;
            view.OnCreationCompleted += CreateActor;
        }

        private void CreateActor(CreationData data)
        {
            view.ShowLoading();
            Debug.LogWarning("Creating acctor:" + data);
        }

        private void PresentView()
        {
            view.InitView();
            playerRepository.Get()
                .Do(player => actorRepository.Get(player.id)
                        .Do(PresentMainGame)
                        .DoWhenAbsent(PresentActorCreationScreen))
                .DoWhenAbsent(PresentLoginScreen);
        }

        private void PresentActorCreationScreen()
        {
            view.ShowActorCreationScreen();
        }

        private void PresentMainGame(Actor actor)
        {
            view.StartMainGame();
        }

        private void PresentLoginScreen()
        {
            view.ShowLoginScreen();
        }

        private void ProcessLogin(LoginData data)
        {
            view.ShowLoading();
            requestLogin.Execute(data)
                .Do(ProcessLoginResponse)
                .Subscribe()
                .AddTo(loginDisposer);
        }

        private void ProcessLoginResponse(LoginResponse response)
        {
            view.HideLoading();
            if (!response.success) view.ShowFailedLoginFeedback(response.message);
            else
            {
                view.ShowSuccessLoginFeedback();
                PresentView();
            }
            
            loginDisposer.DisposeAll();
        }

        private void DisposeView()
        {
            
        }
    }
}