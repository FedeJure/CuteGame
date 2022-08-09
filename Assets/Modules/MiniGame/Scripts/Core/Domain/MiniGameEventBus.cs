using System;
using System.Collections.Generic;
using Modules.MiniGame.Scripts.Presentation;
using UniRx;

namespace Modules.MiniGame.Scripts.Core.Domain
{
    public class MiniGameEventBus
    {
        private ISubject<int> onScoreChange = new Subject<int>();
        private ISubject<float> onStabilityChange = new Subject<float>();
        private ISubject<List<MiniGameUiFeature>> onNewGameStarted = new Subject<List<MiniGameUiFeature>>();
        private ISubject<Unit> onGameEnded = new Subject<Unit>();
        
        public IObservable<int> OnScoreChange()
        {
            return onScoreChange;
        }

        public void EmitOnScoreChange(int score)
        {
            onScoreChange.OnNext(score);
        }

        public IObservable<List<MiniGameUiFeature>> OnNewGameStarted()
        {
            return onNewGameStarted;
        }

        public IObservable<Unit> OnGameEnded() {
            return onGameEnded;
        }

        public void EmitGameEnded() {
            onGameEnded.OnNext(Unit.Default);
        }

        public void EmitOnNewGameStarted(List<MiniGameUiFeature> features)
        {
            onNewGameStarted.OnNext(features);
        }

        public IObservable<float> OnStabilityChange()
        {
            return onStabilityChange;
        }

        public void EmitOnStabilityChange(float value)
        {
            onStabilityChange.OnNext(value);
        }
    }
}