using System;
using UniRx;

namespace Modules.UlaGame.Scripts.Core.Domain
{
    public class UlaGameEventBus
    {
        ISubject<int> onNewStage = new Subject<int>();
        ISubject<float> onStabilityAffected = new Subject<float>();
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
    }
}