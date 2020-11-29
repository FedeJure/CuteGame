using System;
using UniRx;

namespace Modules.UlaGame.Scripts.Core.Domain
{
    public class UlaGameEventBus
    {
        ISubject<int> onNewStage = new Subject<int>();
        ISubject<float> onStabilityAffected = new Subject<float>();
        ISubject<Unit> onGameEnded = new Subject<Unit>();

        public UlaGameEventBus() {}
        public virtual void EmitNewStage(int stage)
        {
            onNewStage.OnNext(stage);   
        }

        public virtual IObservable<int> OnNewStage()
        {
            return onNewStage;
        }

        public virtual void EmitStabilityAffected(float stability)
        {
            onStabilityAffected.OnNext(stability);
        }

        public virtual IObservable<float> OnStabilityAffected()
        {
            return onStabilityAffected;
        }

        public virtual void EmitGameEnded()
        {
            onGameEnded.OnNext(Unit.Default);
        }

        public virtual IObservable<Unit> OnGameEnded()
        {
            return onGameEnded;
        }
    }
}