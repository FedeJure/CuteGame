using System;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;

namespace Modules.ActorModule.Scripts.Core.Domain.Action
{
    public class RetrieveActor
    {
        private readonly ActorRepository actorRepository;
        private readonly SessionRepository sessionRepository;
        
        public RetrieveActor() { }

        public RetrieveActor(ActorRepository actorRepository, SessionRepository sessionRepository)
        {
            this.actorRepository = actorRepository;
            this.sessionRepository = sessionRepository;
        }

        public IObservable<Maybe<Actor>> Execute()
        {
            return sessionRepository.Get().ReturnOrException(
                session => actorRepository.Get(session.actorId),
                new Exception("No session founded"));
        }
    }
}