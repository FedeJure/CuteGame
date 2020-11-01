using System;
using UniRx;

namespace Modules.Actor.Scripts.Core
{
    public class EventBus
    {
        ISubject<Unit> caressSubject = new Subject<Unit>();
        ISubject<Unit> notHappySubject = new Subject<Unit>();
        ISubject<Unit> happySubject = new Subject<Unit>();
        
        public EventBus() { }

        public virtual IObservable<Unit> OnCaressEvent()
        {
            return caressSubject;
        }

        public virtual void EmitOnCaressEvent()
        {
            caressSubject.OnNext(Unit.Default);
        }

        public virtual IObservable<Unit> OnNotHappyEvent()
        {
            return notHappySubject;
        }

        public virtual void EmitNotHappyEvent()
        {
            notHappySubject.OnNext(Unit.Default);
        }

        public virtual IObservable<Unit> OnHappyEvent()
        {
            return happySubject;
        }
    }
}