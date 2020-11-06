using System;
using System.Collections.Generic;
using Modules.Actor.Scripts.Core.Domain.Events;
using UniRx;
using UnityEngine;

namespace Modules.Actor.Scripts.Core
{
    public class EventBus
    {
        Dictionary<Type, ISubject<Unit>> events = new Dictionary<Type, ISubject<Unit>>();

        public EventBus()
        {
            events.Add(typeof(LeftCaressInteractionEvent), new Subject<Unit>());
            events.Add(typeof(RigthCaressInteractionEvent), new Subject<Unit>());
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