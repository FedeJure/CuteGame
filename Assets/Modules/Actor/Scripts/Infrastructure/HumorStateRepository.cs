using Modules.Actor.Scripts.Core.Domain;
using Modules.Common;

namespace Modules.Actor.Scripts.Infrastructure
{
    public interface HumorStateRepository
    {
        void Save(HumorState state);
        Maybe<HumorState> Get();
    }
}