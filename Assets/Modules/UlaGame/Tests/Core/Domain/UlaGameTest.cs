using Modules.UlaGame.Scripts.Core.Domain;
using NSubstitute;
using NUnit.Framework;
using UniRx;

namespace Modules.UlaGame.Tests.Core.Domain
{
    public class UlaGameTest
    {
        private Subject<long> stageIncreaseInterval;
        private Subject<long> affectInterval;
        private UlaGameEventBus eventBus;
        private Scripts.Core.Domain.UlaGame game;
        private float baseAffect = 0.5f;
        private float limitAffect = 10f;

        [SetUp]
        public void SetUp()
        {
            stageIncreaseInterval = new Subject<long>();
            affectInterval = new Subject<long>();
            eventBus = Substitute.For<UlaGameEventBus>();
            game = new Scripts.Core.Domain.UlaGame(eventBus, 
                stageIncreaseInterval,
                affectInterval,
                baseAffect,
                limitAffect);
        }

        [Test]
        public void emit_affect_event_on_interval_emit()
        {
            WhenAffectIntervalEmitted();
            ThenAffectStabilityEventRaised();
        }

        [Test]
        public void emit_new_stage_on_stage_emitted()
        {
            WhenNewStageEmitted();
            ThenNewStageEventRaised(1);
        }

        [Test]
        public void emit_end_game_on_stability_limit_reached()
        {
            GivenAlmostEndedGame();
            WhenAffectIntervalEmitted();
            ThenGameEndedEmitted();
        }

        private void GivenAlmostEndedGame()
        {
            game = new Scripts.Core.Domain.UlaGame(eventBus, 
                stageIncreaseInterval,
                affectInterval,
                baseAffect,
                baseAffect);
        }

        private void ThenGameEndedEmitted()
        {
            eventBus.Received(1).EmitGameEnded();
        }

        private void WhenNewStageEmitted()
        {
            stageIncreaseInterval.OnNext(1);
        }

        private void ThenNewStageEventRaised(int prevStag)
        {
            eventBus.Received(1).EmitNewStage(prevStag + 1);
            Assert.AreEqual(prevStag + 1, game.stage);
        }

        private void WhenAffectIntervalEmitted()
        {
            affectInterval.OnNext(1);
        }

        private void ThenAffectStabilityEventRaised()
        {
            eventBus.Received(2).EmitStabilityAffected(Arg.Any<float>());
        }
    }
}