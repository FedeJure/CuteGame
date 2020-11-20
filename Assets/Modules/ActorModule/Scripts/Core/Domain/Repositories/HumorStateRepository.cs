using Modules.Common;

namespace Modules.ActorModule.Scripts.Core.Domain.Repositories
{
    public interface HumorStateRepository
    {
        void Save(HumorState state, long actorId);
        Maybe<HumorState> Get(long actorId);
    }
}