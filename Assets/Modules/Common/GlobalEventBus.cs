using System;
using UniRx;

namespace Modules.Common
{
    public class GlobalEventBus
    {
        private ISubject<Unit> onMainGameStarted = new Subject<Unit>();
        private ISubject<Unit> onMiniGameStarted = new Subject<Unit>();
        private ISubject<Unit> onMiniGameEnded = new Subject<Unit>();
        
        public void EmitOnMainGameStarted()
        {
            onMainGameStarted.OnNext(Unit.Default);
        }

        public IObservable<Unit> OnMainGameStarted()
        {
            return onMainGameStarted;
        }

        public virtual void EmitOnMiniGameStarted()
        {
            onMiniGameStarted.OnNext(Unit.Default);
        }

        public virtual IObservable<Unit> OnMiniGameStarted()
        {
            return onMiniGameStarted;
        }

        public virtual IObservable<Unit> OnMiniGameEnded()
        {
            return onMiniGameEnded;
        }
        
        public virtual void EmitOnMiniGameEnded()
        {
            onMiniGameEnded.OnNext(Unit.Default);
        }
    }
}