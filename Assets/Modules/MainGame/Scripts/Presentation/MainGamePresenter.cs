using System;
using System.Collections.Generic;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Action;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
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
        private readonly ActorRepository actorRepository;
        private readonly RequestLogin requestLogin;
        private readonly CreateNewActor createNewActor;
        private readonly GlobalEventBus eventBus;
        private bool servicesInitted = false;

        List<IDisposable> loginDisposer = new List<IDisposable>();
        List<IDisposable> creationDisposer = new List<IDisposable>();

        public MainGamePresenter(MainGameView view,
            PlayerRepository playerRepository,
            ActorRepository actorRepository,
            RequestLogin requestLogin,
            CreateNewActor createNewActor,
            GlobalEventBus eventBus)
        {
            this.view = view;
            this.playerRepository = playerRepository;
            this.actorRepository = actorRepository;
            this.requestLogin = requestLogin;
            this.createNewActor = createNewActor;
            this.eventBus = eventBus;

            view.OnViewEnable += PresentView;
            view.OnViewDisable += DisposeView;
            view.OnLoginClicked += ProcessLogin;
            view.OnCreationCompleted += CreateActor;
        }

        private async void PresentView()
        {
            if (!servicesInitted)
            {
                await GooglePlayServicesManager.InitializePlayGamesLogin();
                servicesInitted = true;
            }
            DisposeView();
            view.InitView();
            playerRepository.Get()
                .Do(player =>
                    {
                        
                        var loginFlow = GooglePlayServicesManager.ExistSession()
                            ? requestLogin.Execute().AsUnitObservable()
                            : LoginFlow().AsUnitObservable();
                        
                        loginFlow.SelectMany(_ => actorRepository.Get(player.id))
                            .Do(actorMaybe =>
                            {
                                view.HideLoading();
                                actorMaybe.Do(PresentMainGame)
                                    .DoWhenAbsent(PresentActorCreationScreen);
                            })
                            .DoOnSubscribe(() => view.ShowLoading())
                            .Subscribe().AddTo(loginDisposer);
                    }
                    )
                .DoWhenAbsent(PresentLoginScreen); 
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
            
            createNewActor.Execute(data.name, data.bodySkin.key, data.headSkin.key)
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
