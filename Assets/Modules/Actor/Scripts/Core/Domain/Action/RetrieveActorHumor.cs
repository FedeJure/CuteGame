using Modules.Actor.Scripts.Core.Domain.Repositories;
using Modules.Actor.Scripts.Infrastructure;
using Modules.Common;

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
        public virtual Maybe<HumorState> Execute()
        {
            return humorStateRepository.Get();
        }
    }
}