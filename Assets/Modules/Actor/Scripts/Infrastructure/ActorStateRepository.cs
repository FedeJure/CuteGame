using Modules.Actor.Scripts.Core.Domain;

namespace Modules.Actor.Scripts.Infrastructure
{
    public interface ActorStateRepository
    {
        void Save(ActorState state);
        ActorState Get();
    }
}