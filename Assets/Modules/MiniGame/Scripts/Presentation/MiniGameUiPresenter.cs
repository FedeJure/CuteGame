using System;
using System.Collections.Generic;
using Modules.MiniGame.Scripts.Core.Domain;
using UniRx;

namespace Modules.MiniGame.Scripts.Presentation
{
    public class MiniGameUiPresenter
    {
        private MiniGameUiView view;
        private MiniGameEventBus eventBus;
        
        private List<IDisposable> disposer = new List<IDisposable>();

        public MiniGameUiPresenter(MiniGameUiView view, MiniGameEventBus eventBus)
        {
            this.view = view;
            this.eventBus = eventBus;

            this.view.OnViewEnabled += PresentView;

            eventBus.OnNewGameStarted()
                .Do(InitFeatures)
                .Subscribe()
                .AddTo(disposer);
            
            eventBus.OnScoreChange()
                .Do(UpdateScore)
                .Subscribe()
                .AddTo(disposer);

            eventBus.OnStabilityChange()
                .Do(UpdateStability)
                .Subscribe()
                .AddTo(disposer);
        }

        private void UpdateStability(float stability)
        {
            view.UpdateStability(stability, 30f);
        }

        private void PresentView()
        {
            view.InitView();
        }

        private void InitFeatures(List<MiniGameUiFeature> features)
        {
            features.ForEach(feature =>
            {
                switch (feature)
                {
                    case MiniGameUiFeature.SCORE:
                        view.InitScoreFeature();
                        break;
                    case MiniGameUiFeature.STABILITY:
                        view.InitStability();
                        break;
                }
            });
        }

        private void UpdateScore(int score)
        {
            view.UpdateScore(score);
        }
    }
}