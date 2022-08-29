using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;
using Modules.Services;
using UnityEngine;

namespace Modules.ActorModule.Scripts.Infrastructure
{
    public class RemoteActorRepository: ActorRepository
    {
        private const string KEY = "actor_data";
        public void Save(Actor actor)
        {
            UnityServicesManager.Save(KEY, actor.ToString());
        }

        public Maybe<Actor> Get(string playerId)
        {
            var actorData = UnityServicesManager.Get(KEY).Value;
            return actorData == null ? Maybe<Actor>.Nothing : JsonUtility.FromJson<Actor>(actorData).ToMaybe();
        }
    }
}