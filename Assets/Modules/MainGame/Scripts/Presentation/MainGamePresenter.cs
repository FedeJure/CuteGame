using System;
using System.Collections.Generic;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Action;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;
using Modules.MainGame.Scripts.Core.Actions;
using Modules.PlayerModule.Scripts.Core.Domain.Repositories;
using UniRx;

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

        private void PresentView()
        {
            DisposeView();
            view.InitView();
            playerRepository.Get()
                .Do(player => actorRepository.Get(player.id)
                    .Do(PresentMainGame)
                    .DoWhenAbsent(PresentActorCreationScreen))
                .DoWhenAbsent(PresentLoginScreen);
        }

        private void PresentActorCreationScreen()
        {
            view.MoveCameraToGame()
                .Last()
                .Do(_ => view.ShowActorCreationScreen())
                .Subscribe()
                .AddTo(creationDisposer);
        }

        private void PresentMainGame(Actor actor)
        {
            view.MoveCameraToGame()
                .Last()
                .Do(_ =>
                {
                    view.StartMainGame();
                    eventBus.EmitOnMainGameStarted();
                })
                .Subscribe()
                .AddTo(loginDisposer);
            
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
