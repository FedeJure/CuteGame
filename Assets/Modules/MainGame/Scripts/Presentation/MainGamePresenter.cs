using System;
using System.Collections.Generic;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Action;
using Modules.Common;
using Modules.MainGame.Scripts.Core.Actions;
using Modules.PlayerModule.Scripts.Core.Domain.Repositories;
using Modules.Services;
using UniRx;
using UnityEngine;

namespace Modules.MainGame.Scripts.Presentation
{
    public class MainGamePresenter
    {
        private readonly MainGameView view;
        private readonly PlayerRepository playerRepository;
        private readonly RequestLogin requestLogin;
        private readonly CreateNewActor createNewActor;
        private readonly GlobalEventBus eventBus;
        private readonly RetrieveActor retrieveActor;
        private bool servicesInitted;

        List<IDisposable> loginDisposer = new List<IDisposable>();
        List<IDisposable> creationDisposer = new List<IDisposable>();

        public MainGamePresenter(MainGameView view,
            PlayerRepository playerRepository,
            RequestLogin requestLogin,
            CreateNewActor createNewActor,
            GlobalEventBus eventBus,
            RetrieveActor retrieveActor)
        {
            this.view = view;
            this.playerRepository = playerRepository;
            this.requestLogin = requestLogin;
            this.createNewActor = createNewActor;
            this.eventBus = eventBus;
            this.retrieveActor = retrieveActor;

            view.OnViewEnable += PresentView;
            view.OnViewDisable += DisposeView;
            view.OnLoginClicked += ProcessLogin;
            view.OnCreationCompleted += CreateActor;
        }

        private async void PresentView()
        {
            try
            {
                if (!servicesInitted)
                {
                    await GooglePlayServicesManager.InitializePlayGamesLogin();
                    servicesInitted = true;
                }
                DisposeView();
                view.InitView();
                var player = playerRepository.Get();
                if (!player.hasValue) PresentLoginScreen();
                else
                {
                
                    var loginFlow = GooglePlayServicesManager.ExistSession()
                        ? requestLogin.Execute().AsUnitObservable()
                        : LoginFlow().AsUnitObservable();
                        
                    loginFlow
                        .DoOnSubscribe(() => view.ShowLoading())
                        .SelectMany(_ =>
                        {
                            return retrieveActor.Execute();
                        })
                        .Select(actorMaybe =>
                        {
                            view.HideLoading();
                            actorMaybe.Do(PresentMainGame)
                                .DoWhenAbsent(PresentActorCreationScreen);
                            return Unit.Default;
                        })
                        .Subscribe()
                        .AddTo(loginDisposer);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.StackTrace);
            }
            
        }

        private void PresentActorCreationScreen()
        {
            view.MoveCameraToCreationView()
                .Last()
                .Do(_ => view.ShowActorCreationScreen())
                .Subscribe()
                .AddTo(creationDisposer);
        }

        private void PresentMainGame(Actor actor)
        {
            eventBus.EmitOnMainGameStarted();
            view.StartMainGame();
            view.MoveCameraToMainGame()
                .Last()
                .Do(_ =>
                {
                    view.StartMainGame();
                    DisposeView();
                })
                .Subscribe()
                .AddTo(loginDisposer);
            
        }

        private void PresentLoginScreen()
        {
            view.ShowLoginScreen();
        }

        private IObservable<LoginResponse> LoginFlow()
        {
            return requestLogin.Execute()
                .DoOnSubscribe(() => view.ShowLoading())
                .Do(ProcessLoginResponse);
        }

        private void ProcessLogin()
        {
            LoginFlow()
                .Subscribe()
                .AddTo(loginDisposer);
        }
        private void ProcessLoginResponse(LoginResponse response)
        {
            view.HideLoading();
            if (!response.success)
            {
                view.ShowFailedLoginFeedback(response.message);
                GooglePlayServicesManager.LogOut();
                PresentView();
            }
            else
            {
                view.ShowSuccessLoginFeedback();
                PresentView();
            }
        }

        private void CreateActor(CreationData data)
        {
            createNewActor.Execute(data.name, data.bodySkin, data.headSkin)
                .Do(_ => PresentView())
                .Subscribe()
                .AddTo(creationDisposer);
        }

        private void DisposeView()
        {
            loginDisposer.DisposeAll();
            creationDisposer.DisposeAll();
        }
    }
}