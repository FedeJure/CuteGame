using Modules.Actor.Scripts.Core.Domain;

namespace Modules.Actor.Scripts.Infrastructure
{
    public class InMemoryHumorStateRepository: HumorStateRepository
    {
        HumorState state;
        public void Save(HumorState state)
        {
            this.state = state;
        }

        public HumorState Get()
        {
            return state;
        }
    }
}