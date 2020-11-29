using System;
using Modules.Common;
using Modules.MiniGame.Scripts.Presentation;
using NSubstitute;
using NUnit.Framework;

namespace Modules.MiniGame.Tests.Presentation
{
    public class MiniGamePresenterTest
    {
        private GlobalEventBus globalEventBus;
        private MiniGameView view;

        [SetUp]
        public void SetUp()
        {
            globalEventBus = Substitute.For<GlobalEventBus>();
            view = Substitute.For<MiniGameView>();
            new MiniGamePresenter(view, globalEventBus);
        }

        [Test]
        public void emit_event_on_game_presented()
        {
            WhenViewEnabled();
            ThenStartMinigameEventEmited();
        }

        [Test]
        public void emit_event_on_game_dispose()
        {
            WhenViewDisabled();
            ThenEndMinigameEventEmitted();
        }

        private void WhenViewDisabled()
        {
            view.OnViewDisabled += Raise.Event<Action>();
        }

        private void ThenEndMinigameEventEmitted()
        {
            globalEventBus.Received(1).EmitOnMiniGameEnded();
        }

        private void WhenViewEnabled()
        {
            view.OnViewEnabled += Raise.Event<Action>();
        }

        private void ThenStartMinigameEventEmited()
        {
            globalEventBus.Received(1).EmitOnMiniGameStarted();
        }
    }
}