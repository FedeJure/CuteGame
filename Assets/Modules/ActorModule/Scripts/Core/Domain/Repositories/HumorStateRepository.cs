using Modules.Common;

namespace Modules.ActorModule.Scripts.Core.Domain.Repositories
{
    public interface HumorStateRepository
    {
        void Save(HumorState state, string actorId);
        Maybe<HumorState> Get(string actorId);
    }
}