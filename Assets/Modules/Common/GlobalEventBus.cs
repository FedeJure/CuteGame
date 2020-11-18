using System;
using UniRx;

namespace Modules.Common
{
    public class GlobalEventBus
    {
        private ISubject<Unit> onMainGameStarted = new Subject<Unit>();
        
        public void EmitOnMainGameStarted()
        {
            onMainGameStarted.OnNext(Unit.Default);
        }

        public IObservable<Unit> OnMainGameStarted()
        {
            return onMainGameStarted;
        }
    }
}