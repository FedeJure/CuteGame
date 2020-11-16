using Modules.Common;

namespace Modules.ActorModule.Scripts.Core.Domain.Repositories
{
    public interface ActorRepository
    {
        void Save(Actor actor);
        Maybe<Actor> Get(long playerId);
    }
}