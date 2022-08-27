using System;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Action;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;
using Modules.MainGame.Scripts.Core.Actions;
using Modules.MainGame.Scripts.Presentation;
using Modules.PlayerModule.Scripts.Core.Domain;
using Modules.PlayerModule.Scripts.Core.Domain.Repositories;
using NSubstitute;
using NUnit.Framework;
using UniRx;

namespace Modules.MainGame.Tests.Presentation
{
    public class MainGamePresenterTest
    {
        string ACTOR_ID = "ACTOR ID";
        string PLAYER_ID = "PLAYER ID";
        private MainGameView view;
        private PlayerRepository playerRepository;
        private ActorRepository actorRepository;
        private RequestLogin requestLogin;

        private ISubject<LoginResponse> loginReponse = new Subject<LoginResponse>();
        private CreateNewActor createActor;
        private GlobalEventBus eventBus;

        [SetUp]
        public void SetUp()
        {
            view = Substitute.For<MainGameView>();
            playerRepository = Substitute.For<PlayerRepository>();
            actorRepository = Substitute.For<ActorRepository>();
            requestLogin = Substitute.For<RequestLogin>();
            createActor = Substitute.For<CreateNewActor>();
            eventBus = Substitute.For<GlobalEventBus>();
            
            requestLogin.Execute(Arg.Any<LoginData>()).Returns(loginReponse);
            new MainGamePresenter(view, playerRepository, actorRepository, requestLogin, createActor, eventBus);
        }

        [Test]
        public void start_game_without_player_logged()
        {
            GivenNoPlayerLogged();
            WhenPresent();
            ThenShowLoginScreen();
        }

        [Test]
        public void start_game_with_player_logged_and_no_actor_created()
        {
            GivenPlayerLogged();
            GivenNoActorCreated();
            WhenPresent();
            ThenShowActorCreationScreen();
        }

        [Test]
        public void start_game_with_player_logged_and_actor_created()
        {
            GivenPlayerLogged();
            GivenActorCreated();
            WhenPresent();
            ThenStartMainGame();
        }

        [Test]
        public void request_login_show_loading_screen()
        {
            GivenNoPlayerLogged();
            WhenLoginButtonClicked();
            ThenShowLoadingPaneScreen();
        }

        [Test]
        public void request_login_on_login_clicked()
        {
            GivenNoPlayerLogged();
            WhenLoginButtonClicked();
            ThenApiLoginRequested();
        }

        [Test]
        public void request_login_receive_failed_response()
        {
            GivenNoPlayerLogged();
            GivenLoginButtonClicked();
            WhenFailedLoginResponseArrived();
            ThenShowFailedFeedback();
        }

        [Test]
        public void request_login_receive_success_response()
        {
            GivenNoPlayerLogged();
            GivenLoginButtonClicked();
            WhenSuccessLoginResponseArrived();
            ThenShowSuccessFeedback();
        }

        [Test]
        public void dispose_loading_on_receive_loading_response()
        {
            GivenNoPlayerLogged();
            GivenLoginButtonClicked();
            WhenSuccessLoginResponseArrived();
            ThenHideLoadingView();
        }

        private void ThenHideLoadingView()
        {
            view.Received(1).HideLoading();
        }

        private void ThenShowLoadingPaneScreen()
        {
            view.Received(1).ShowLoading();
        }

        private void ThenShowSuccessFeedback()
        {
            view.Received(1).ShowSuccessLoginFeedback();
        }

        private void WhenSuccessLoginResponseArrived()
        {
            loginReponse.OnNext(new LoginResponse(true, "",Substitute.For<Player>(), Substitute.For<Actor>()));
        }

        private void GivenLoginButtonClicked()
        {
            view.OnLoginClicked += Raise.Event<Action<LoginData>>(new LoginData("",""));
        }

        private void WhenFailedLoginResponseArrived()
        {
            loginReponse.OnNext(new LoginResponse(false, "",Substitute.For<Player>(), Substitute.For<Actor>()));
        }

        private void ThenShowFailedFeedback()
        {
            view.Received(1).ShowFailedLoginFeedback(Arg.Any<string>());
        }


        
        private void WhenLoginButtonClicked()
        {
            view.OnLoginClicked += Raise.Event<Action<LoginData>>(new LoginData("",""));
        }

        private void ThenApiLoginRequested()
        {
            requestLogin.Received(1).Execute(Arg.Any<LoginData>());
        }

        private void GivenActorCreated()
        {
            actorRepository.Get(PLAYER_ID).Returns(new Actor(ACTOR_ID, "name", new ActorSkin("", ""), new Player(PLAYER_ID)).ToMaybe());
        }

        private void ThenStartMainGame()
        {
            view.Received(1).StartMainGame();
        }

        private void GivenNoActorCreated()
        {
            actorRepository.Get(PLAYER_ID).Returns(Maybe<Actor>.Nothing);
        }

        private void GivenNoPlayerLogged()
        {
            playerRepository.Get().Returns(Maybe<Player>.Nothing);
        }

        private void WhenPresent()
        {
            view.OnViewEnable += Raise.Event<Action>();
        }

        private void ThenShowLoginScreen()
        {
            view.Received(1).ShowLoginScreen();
        }

        private void GivenPlayerLogged()
        {
            playerRepository.Get().Returns(new Player(PLAYER_ID).ToMaybe());
        }

        private void ThenShowActorCreationScreen()
        {
            view.Received(1).ShowActorCreationScreen();
        }
    }
}