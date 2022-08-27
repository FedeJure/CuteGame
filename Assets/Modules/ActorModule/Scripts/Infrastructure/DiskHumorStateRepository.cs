using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;
using UnityEngine;

namespace Modules.ActorModule.Scripts.Infrastructure
{
    public class DiskHumorStateRepository : HumorStateRepository
    {
        private const string key = "humor_state";
        public void Save(HumorState state, string actorId)
        {
            LocalStorage.Save(GetKey(actorId), state.ToString());
        }

        public Maybe<HumorState> Get(string actorId)
        {
            return LocalStorage.Get(GetKey(actorId))
                .ReturnOrDefault(value => new Maybe<HumorState>(JsonUtility.FromJson<HumorState>(value)),
                    Maybe<HumorState>.Nothing);
        }

        private string GetKey(string actorId)
        {
            return $"{key}:actorId:{actorId}";
        }
    }
}