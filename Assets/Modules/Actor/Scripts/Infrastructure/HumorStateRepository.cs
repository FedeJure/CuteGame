using Modules.Actor.Scripts.Core.Domain;

namespace Modules.Actor.Scripts.Infrastructure
{
    public interface HumorStateRepository
    {
        void Save(HumorState state);
        HumorState Get();
    }
}