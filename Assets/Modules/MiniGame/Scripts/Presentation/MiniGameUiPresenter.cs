using System;
using System.Collections.Generic;
using Castle.Core;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Action;
using Modules.MiniGame.Scripts.Core.Domain;
using UniRx;

namespace Modules.MiniGame.Scripts.Presentation
{
    public class MiniGameUiPresenter
    {
        private MiniGameUiView view;
        private UpdateHumorFromScore updateHumorFromScore;
        
        private List<IDisposable> disposer = new List<IDisposable>();
        private List<MiniGameUiFeature> visibleFeatures = new List<MiniGameUiFeature>();

        public MiniGameUiPresenter(MiniGameUiView view, MiniGameEventBus eventBus, UpdateHumorFromScore updateHumorFromScore)
        {
            this.view = view;
            this.updateHumorFromScore = updateHumorFromScore;

            this.view.OnViewEnabled += PresentView;

            eventBus.OnNewGameStarted()
                .Do(InitFeatures)
                .Subscribe()
                .AddTo(disposer);

            eventBus.OnGameEnded()
                .Do(_ => DisposeFeatures())
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
            view.UpdateStability(0, 30f);
        }

        private void InitFeatures(List<MiniGameUiFeature> features)
        {
            features.ForEach(feature =>
            {
                switch (feature)
                {
                    case MiniGameUiFeature.SCORE:
                        visibleFeatures.Add(MiniGameUiFeature.SCORE);
                        view.InitScoreFeature();
                        break;
                    case MiniGameUiFeature.STABILITY:
                        visibleFeatures.Add(MiniGameUiFeature.STABILITY);
                        view.InitStability();
                        break;
                }
            });
        }

        private void DisposeFeatures()
        {
            visibleFeatures.ForEach(feature =>
            {
                switch (feature)
                {
                    case MiniGameUiFeature.SCORE:
                        view.DisposeScoreFeature();
                        break;
                    case MiniGameUiFeature.STABILITY:
                        view.DisposeStability();
                        break;
                }
            });
            visibleFeatures.Clear();
        }

        private void UpdateScore(Pair<float, float> score)
        {
            view.UpdateScore(score.First);
            updateHumorFromScore.Execute(score.Second / 10);
        }
    }
}