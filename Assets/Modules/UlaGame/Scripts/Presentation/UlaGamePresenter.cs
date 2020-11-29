using System;
using System.Collections.Generic;
using Modules.Common;
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
        
        private List<IDisposable> disposer = new List<IDisposable>();

        public UlaGamePresenter(UlaGameView view,
            GlobalEventBus globalEventBus,
            StartUlaGameAction startUlaGame,
            UlaGameEventBus eventBus) : base(view, globalEventBus)
        {
            this.view = view;
            this.startUlaGame = startUlaGame;
            this.eventBus = eventBus;
        }

        protected override void PresentView()
        {
            eventBus.OnStabilityAffected()
                .Do(currentStability => view.SetStability(currentStability))
                .Subscribe()
                .AddTo(disposer);

            eventBus.OnNewStage()
                .Do(stage => view.SetStage(stage))
                .Subscribe()
                .AddTo(disposer);

            eventBus.OnGameEnded()
                .Do(_ => view.EndGame())
                .Subscribe()
                .AddTo(disposer);
            
            startUlaGame.Execute();
        }

        protected override void DisposeView()
        {
            disposer.DisposeAll();
        }
    }
}