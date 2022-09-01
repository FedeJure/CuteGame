using System;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;
using Modules.Services;
using UniRx;
using UnityEngine;

namespace Modules.ActorModule.Scripts.Infrastructure
{
    public class RemoteActorRepository: ActorRepository
    {
        private const string KEY = "actor_data";
        public IObservable<Unit> Save(Actor actor)
        {
           return UnityServicesManager.Save(KEY, actor.ToString());
        }

        public IObservable<Maybe<Actor>> Get(string playerId)
        {
            return UnityServicesManager.Get(KEY)
                .Select(value => value.Select(v => JsonUtility.FromJson<Actor>(value.Value)));
        }
    }
}