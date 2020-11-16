using System;
using System.Collections.Generic;
using Modules.ActorModule.Scripts.Core.Domain;
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
            requestLogin.Execute(data)
                .Do(ProcesLoginResponse)
                .Subscribe()
                .AddTo(loginDisposer);
        }

        private void ProcesLoginResponse(LoginResponse response)
        {
            if (!response.success)
            {
                view.ShowFailedLoginFeedback(response.message);
            }
            
            loginDisposer.DisposeAll();
        }

        private void DisposeView()
        {
            
        }
    }
}
