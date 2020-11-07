using Modules.Actor.Scripts.Core.Domain;

namespace Modules.Actor.Scripts.Infrastructure
{
    public class InMemoryHumorStateRepository: HumorStateRepository
    {
        HumorState state;
        public virtual void Save(HumorState state)
        {
            this.state = state;
        }

        public virtual HumorState Get()
        {
            return state;
        }
    }
}