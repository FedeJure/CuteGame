using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;
using UnityEngine;

namespace Modules.ActorModule.Scripts.Infrastructure
{
    public class DiskHumorStateRepository : HumorStateRepository
    {
        private const string key = "humor_state";
        public void Save(HumorState state, long actorId)
        {
            LocalStorage.Save(GetKey(actorId), state.ToString());
        }

        public Maybe<HumorState> Get(long actorId)
        {
            return LocalStorage.Get(GetKey(actorId))
                .ReturnOrDefault(value => new Maybe<HumorState>(JsonUtility.FromJson<HumorState>(value)),
                    Maybe<HumorState>.Nothing);
        }

        private string GetKey(long actorId)
        {
            return $"{key}:actorId:{actorId}";
        }
    }
}