using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;

namespace Modules.ActorModule.Scripts.Core.Domain.Action
{
    public class RetrieveActorHumor
    {
        private readonly HumorStateRepository humorStateRepository;
        private readonly SessionRepository sessionRepository;
        public RetrieveActorHumor() { }

        public RetrieveActorHumor(HumorStateRepository humorStateRepository, SessionRepository sessionRepository)
        {
            this.humorStateRepository = humorStateRepository;
            this.sessionRepository = sessionRepository;
        }
        public virtual Maybe<HumorState> Execute()
        {
            return sessionRepository.Get().ReturnOrDefault(session =>
                humorStateRepository.Get(session.actorId),
                Maybe<HumorState>.Nothing);
        }
    }
}