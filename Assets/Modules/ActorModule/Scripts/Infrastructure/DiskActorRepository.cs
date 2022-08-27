using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;
using UnityEngine;

namespace Modules.ActorModule.Scripts.Infrastructure
{
    public class DiskActorRepository : ActorRepository
    {
        private static string key = "Actor";
        public void Save(Actor actor)
        {
            LocalStorage.Save(GetKey(actor.owner.id), actor.ToString());
        }

        public Maybe<Actor> Get(string playerId)
        {
            return LocalStorage.Get(GetKey(playerId))
                .ReturnOrDefault(actor => JsonUtility.FromJson<Actor>(actor).ToMaybe(),
                    Maybe<Actor>.Nothing);
        }

        private static string GetKey(string ownerId)
        {
            return $"{key}:id:{ownerId}";
        }
    }
}