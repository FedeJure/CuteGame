﻿using System;
using System.Collections.Generic;
using Modules.Common;
using UniRx;

namespace Modules.UlaGame.Scripts.Core.Domain
{
    public class UlaGame
    {
        private readonly UlaGameEventBus eventBus;
        private readonly IObservable<long> stageIncreasePeriod;
        private readonly IObservable<long> affectStabilityPeriod;
        private int currentPoints { get; } = 0;
        public int stage { get; private set; } = 1;
        
        public float stability { get; private set; }
        private float absoluteStabilityLimit;
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
        }

        private void AffectStability()
        {
            var signFactor = stability == 0 ? 1 : (int)( Math.Abs(stability) / stability);
            EditStability (stability + signFactor * baseStabilityChange * stage);
            eventBus.EmitStabilityAffected(stability);
        }

        private void IncreaseStage()
        {
            stage += 1;
            eventBus.EmitNewStage(stage);
        }

        private void EditStability(float newValue)
        {
            stability = newValue;
            if (Math.Abs(stability) >= absoluteStabilityLimit) EndGame();
        }

        private void EndGame()
        {
            eventBus.EmitGameEnded();
            disposer.DisposeAll();
        }
    }
}