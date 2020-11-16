using System;
using System.Collections.Generic;
using Modules.ActorModule.Scripts.Core.Domain.Events;
using UniRx;

namespace Modules.ActorModule.Scripts.Core.Domain
{
    public class EventBus
    {
        Dictionary<Type, ISubject<Unit>> events = new Dictionary<Type, ISubject<Unit>>();

        public EventBus()
        {
            events.Add(typeof(LeftCaressInteractionEvent), new Subject<Unit>());
            events.Add(typeof(RigthCaressInteractionEvent), new Subject<Unit>());
            events.Add(typeof(LeftTickleInteractionEvent), new Subject<Unit>());
            events.Add(typeof(RightTickleInteractionEvent), new Subject<Unit>());
            events.Add(typeof(MiddleConsentEvent), new Subject<Unit>());
            events.Add(typeof(HappyEvent), new Subject<Unit>());
            events.Add(typeof(NotHappyEvent), new Subject<Unit>());
            events.Add(typeof(HumorChangesEvent), new Subject<Unit>());
        }

        public virtual IObservable<Unit> OnEvent<T>()
        {
            CheckEvent<T>();
            return events[typeof(T)];
        }

        public virtual void EmitEvent<T>()
        {
            CheckEvent<T>();
            events[typeof(T)].OnNext(Unit.Default);
        }
        
        private void CheckEvent<T>()
        {
            if (!events.ContainsKey(typeof(T)))  throw new Exception($"Event of type {typeof(T)} not registered on EventBus");
        }
    }
}