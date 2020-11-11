using Modules.Actor.Scripts.Infrastructure;

namespace Modules.Actor.Scripts.Core.Domain.Action
{
    public class RetrieveActorHumor
    {
        private readonly HumorStateRepository humorStateRepository;
        public RetrieveActorHumor() { }

        public RetrieveActorHumor(HumorStateRepository humorStateRepository)
        {
            this.humorStateRepository = humorStateRepository;
        }
        public virtual HumorState Execute()
        {
            return humorStateRepository.Get();
        }
    }
}