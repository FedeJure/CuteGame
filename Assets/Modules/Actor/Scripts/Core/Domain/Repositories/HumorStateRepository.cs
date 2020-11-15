using Modules.Common;

namespace Modules.Actor.Scripts.Core.Domain.Repositories
{
    public interface HumorStateRepository
    {
        void Save(HumorState state);
        Maybe<HumorState> Get();
    }
}