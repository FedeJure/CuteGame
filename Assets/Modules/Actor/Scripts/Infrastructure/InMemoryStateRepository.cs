using Modules.Actor.Scripts.Core.Domain;

namespace Modules.Actor.Scripts.Infrastructure
{
    public class InMemoryStateRepository: ActorStateRepository
    {
        ActorState state;
        public void Save(ActorState state)
        {
            this.state = state;
        }

        public  ActorState Get()
        {
            return state;
        }
    }
}