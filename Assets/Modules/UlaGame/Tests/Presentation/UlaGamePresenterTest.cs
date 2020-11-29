using System;
using Modules.Common;
using Modules.UlaGame.Scripts.Core.Actions;
using Modules.UlaGame.Scripts.Core.Domain;
using Modules.UlaGame.Scripts.Presentation;
using NSubstitute;
using NUnit.Framework;
using UniRx;

namespace Modules.UlaGame.Tests.Presentation
{
    public class UlaGamePresenterTest
    {
        private UlaGameView view;
        private GlobalEventBus globalEventBus;
        private StartUlaGameAction startGame;
        private UlaGameEventBus eventBus;
        
        private ISubject<float> stabilitySubject = new Subject<float>();
        private ISubject<int> stageSubject = new Subject<int>();
        private ISubject<Unit> gameEndSubject = new Subject<Unit>();

        [SetUp]
        public void SetUp()
        {
            view = Substitute.For<UlaGameView>();
            globalEventBus = Substitute.For<GlobalEventBus>();
            startGame = Substitute.For<StartUlaGameAction>();
            eventBus = Substitute.For<UlaGameEventBus>();

            eventBus.OnGameEnded().Returns(gameEndSubject);
            eventBus.OnNewStage().Returns(stageSubject);
            eventBus.OnStabilityAffected().Returns(stabilitySubject);
            
            new UlaGamePresenter(view, globalEventBus, startGame, eventBus);
        }

        [Test]
        public void init_game_on_present()
        {
            WhenViewEnabled();
            ThenGamePresented();
        }

        [Test]
        public void call_view_on_game_ended()
        {
            GivenGameWasPresented();
            WhenGameEndEventArrived();
            ThenViewGameEndCalled();
        }
        
        [Test]
        public void call_view_on_stability_affected()
        {
            GivenGameWasPresented();
            WhenStabilityAffectedEventArrived();
            ThenViewStabilityAffectedCalled();
        }

        [Test]
        public void call_view_on_stage_changes()
        {
            GivenGameWasPresented();
            WhenStageChangesArrived();
            ThenStageChangeCalled();
        }

        [Test]
        public void emit_swipe_on_receive_from_view()
        {
            GivenGameWasPresented();
            WhenReceiveNewSwipe();
            ThenEmitNewSwipe();
        }

        private void WhenReceiveNewSwipe()
        {
            view.OnSwipeReceived += Raise.Event<Action<TouchDirection>>(TouchDirection.Left);
        }

        private void ThenEmitNewSwipe()
        {
            eventBus.Received(1).EmitNewSwipe(-1);
        }

        private void WhenStabilityAffectedEventArrived()
        {
            stabilitySubject.OnNext(1f);
        }

        private void ThenViewStabilityAffectedCalled()
        {
            view.Received(1).SetStability(1f);
        }

        private void WhenStageChangesArrived()
        {
            stageSubject.OnNext(1);
        }

        private void ThenStageChangeCalled()
        {
            view.Received(1).SetStage(1);
        }

        private void GivenGameWasPresented()
        {
            view.OnViewEnabled += Raise.Event<Action>();
        }

        private void WhenGameEndEventArrived()
        {
            gameEndSubject.OnNext(Unit.Default);
        }

        private void ThenViewGameEndCalled()
        {
            view.Received(1).EndGame();
        }

        private void ThenGamePresented()
        {
            eventBus.Received(1).OnNewStage();
            eventBus.Received(1).OnGameEnded();
            eventBus.Received(1).OnStabilityAffected();
            startGame.Received(1).Execute();
        }

        private void WhenViewEnabled()
        {
            view.OnViewEnabled += Raise.Event<Action>();
        }
    }
}