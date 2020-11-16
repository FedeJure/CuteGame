using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;
using UnityEngine;

namespace Modules.ActorModule.Scripts.Infrastructure
{
    public class DiskHumorStateRepository : HumorStateRepository
    {
        private const string key = "humor_state";
        public void Save(HumorState state)
        {
            LocalStorage.Save(key, state.ToString());
        }

        public Maybe<HumorState> Get()
        {
            return LocalStorage.Get(key)
                .ReturnOrDefault(value => new Maybe<HumorState>(JsonUtility.FromJson<HumorState>(value)),
                    Maybe<HumorState>.Nothing);
        }
    }
}