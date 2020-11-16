using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;

namespace Modules.ActorModule.Scripts.Infrastructure
{
    public class InMemoryHumorStateRepository: HumorStateRepository
    {
        HumorState state;
        public void Save(HumorState state)
        {
            this.state = state;
        }

        public Maybe<HumorState> Get()
        {
            return state == null ? Maybe<HumorState>.Nothing : new Maybe<HumorState>(state);
        }
    }
}