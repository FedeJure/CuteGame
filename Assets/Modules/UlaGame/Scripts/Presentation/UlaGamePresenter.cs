using System;
using System.Collections.Generic;
using Castle.Core;
using Modules.Common;
using Modules.MiniGame.Scripts.Core.Domain;
using Modules.MiniGame.Scripts.Presentation;
using Modules.UlaGame.Scripts.Core.Actions;
using Modules.UlaGame.Scripts.Core.Domain;
using UniRx;

namespace Modules.UlaGame.Scripts.Presentation
{
    public class UlaGamePresenter : MiniGamePresenter
    {
        private readonly UlaGameView view;
        private readonly StartUlaGameAction startUlaGame;
        private readonly UlaGameEventBus eventBus;
        private readonly MiniGameEventBus miniGameEventBus;

        private List<IDisposable> disposer = new List<IDisposable>();

        public UlaGamePresenter(UlaGameView view,
            GlobalEventBus globalEventBus,
            StartUlaGameAction startUlaGame,
            UlaGameEventBus eventBus, 
            MiniGameEventBus miniGameEventBus) : base(view, globalEventBus)
        {
            this.view = view;
            this.startUlaGame = startUlaGame;
            this.eventBus = eventBus;
            this.miniGameEventBus = miniGameEventBus;

            view.OnSwipeReceived += ReceiveSwipe;
        }

        protected override void PresentView()
        {
            eventBus.OnStabilityAffected()
                .Do(currentStability =>
                {
                    view.SetStability(currentStability);
                    miniGameEventBus.EmitOnStabilityChange(currentStability);
                })
                .Subscribe()
                .AddTo(disposer);

            eventBus.OnNewStage()
                .Do(stage => view.SetStage(stage))
                .Subscribe()
                .AddTo(disposer);

            eventBus.OnGameEnded()
                .Do(_ => view.EndGame())
                .Do(_ => miniGameEventBus.EmitGameEnded())
                .Subscribe()
                .AddTo(disposer);

            eventBus.OnUlaGameStarted()
                .Do(InitView)
                .Subscribe()
                .AddTo(disposer);

            eventBus.OnScoreChange()
                .Do(UpdateScore)
                .Subscribe()
                .AddTo(disposer);
            
            miniGameEventBus.EmitOnNewGameStarted(
                new List<MiniGameUiFeature>
                {
                    MiniGameUiFeature.SCORE, 
                    MiniGameUiFeature.STABILITY
                });

            startUlaGame.Execute();
        }

        private void InitView(Core.Domain.UlaGame game)
        {
            view.Init(game.absoluteStabilityLimit);
        }


        void ReceiveSwipe(TouchDirection action)
        {
            switch (action)
            {
                case TouchDirection.Left:
                    eventBus.EmitNewSwipe(-1);
                    break;
                case TouchDirection.Right:
                    eventBus.EmitNewSwipe(1);
                    break;
            }
        }

        protected override void DisposeView()
        {
            disposer.DisposeAll();
        }

        private void UpdateScore(Pair<float, float> score) {
            miniGameEventBus.EmitOnScoreChange(score.First, score.Second);
        }
    }
}