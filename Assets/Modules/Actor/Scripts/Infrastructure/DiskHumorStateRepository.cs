using Modules.Actor.Scripts.Core.Domain;
using Modules.Common;
using UnityEngine;

namespace Modules.Actor.Scripts.Infrastructure
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