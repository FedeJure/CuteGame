using System;
using System.Collections.Generic;
using Castle.Core;
using Modules.MiniGame.Scripts.Presentation;
using UniRx;

namespace Modules.MiniGame.Scripts.Core.Domain
{
    public class MiniGameEventBus
    {
        private ISubject<Pair<float, float>> onScoreChange = new Subject<Pair<float, float>>();
        private ISubject<float> onStabilityChange = new Subject<float>();
        private ISubject<List<MiniGameUiFeature>> onNewGameStarted = new Subject<List<MiniGameUiFeature>>();
        private ISubject<Unit> onGameEnded = new Subject<Unit>();
        
        public IObservable<Pair<float, float>> OnScoreChange()
        {
            return onScoreChange;
        }

        public void EmitOnScoreChange(float totalScore, float score)
        {
            onScoreChange.OnNext(new Pair<float, float>(totalScore, score));
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