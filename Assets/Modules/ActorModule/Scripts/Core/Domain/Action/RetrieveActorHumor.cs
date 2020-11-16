using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;

namespace Modules.ActorModule.Scripts.Core.Domain.Action
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