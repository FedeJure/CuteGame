using System;
using System.Collections.Generic;
using Modules.Common;
using UniRx;
using UnityEngine;

namespace Modules.UlaGame.Scripts.Core.Domain
{
    public class UlaGame
    {
        private readonly UlaGameEventBus eventBus;
        private readonly IObservable<long> stageIncreasePeriod;
        private readonly IObservable<long> affectStabilityPeriod;
        public int currentPoints { get; private set; } = 0;
        public int stage { get; private set; } = 1;
        
        public float stability { get; private set; }
        public float absoluteStabilityLimit { get; private set; }
        private float baseStabilityChange;

        private readonly List<IDisposable> disposer = new List<IDisposable>();
        
        public UlaGame(UlaGameEventBus eventBus,
            IObservable<long> stageIncreasePeriod,
            IObservable<long> affectStabilityPeriod,
            float baseStabilityChange,
            float absoluteStabilityLimit)
        {
            this.eventBus = eventBus;
            this.stageIncreasePeriod = stageIncreasePeriod;
            this.affectStabilityPeriod = affectStabilityPeriod;
            this.baseStabilityChange = baseStabilityChange;
            this.absoluteStabilityLimit = absoluteStabilityLimit;

            Init();
        }

        private void Init()
        {
            eventBus.EmitNewStage(stage);
            
            stageIncreasePeriod
                .Do(_ => IncreaseStage())
                .Subscribe()
                .AddTo(disposer);

            affectStabilityPeriod
                .Do(_ => AffectStability())
                .Subscribe()
                .AddTo(disposer);

            eventBus.OnNewSwipe()
                .Do(ReceiveSwipe)
                .Subscribe()
                .AddTo(disposer);

            Observable.Interval(TimeSpan.FromSeconds(1))
                .Do(_ => UpdateScore())
                .Subscribe()
                .AddTo(disposer);

            eventBus.EmitUlaGameStared(this);
        }

        private void ReceiveSwipe(int swipe)
        {
            EditStability(stability + swipe);
        }

        private void AffectStability()
        {
            var signFactor = stability == 0 ? 1 : (int)( Math.Abs(stability) / stability);
            EditStability (stability + signFactor * baseStabilityChange * stage);
        }

        private void IncreaseStage()
        {
            stage += 1;
            eventBus.EmitNewStage(stage);
        }

        private void EditStability(float newValue)
        {
            stability = newValue;
            eventBus.EmitStabilityAffected(stability);
            if (Math.Abs(stability) >= absoluteStabilityLimit) EndGame();
        }

        private void EndGame()
        {
            eventBus.EmitGameEnded();
            disposer.DisposeAll();
        }

        private void UpdateScore() {
            currentPoints += (stability >= -absoluteStabilityLimit / 2 && stability <= absoluteStabilityLimit / 2) ? 2 : 1;
            eventBus.EmitScoreChange(currentPoints);
        }
    }
}