using System;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;
using Modules.MainGame.Scripts.Core.Domain;
using Modules.PlayerModule.Scripts.Core.Domain.Repositories;
using UniRx;

namespace Modules.ActorModule.Scripts.Core.Domain.Action
{
    public class CreateNewActor
    {
        private readonly ActorRepository actorRepository;
        private readonly PlayerRepository playerRepository;
        private readonly SessionRepository sessionRepository;

        public CreateNewActor() { }
        public CreateNewActor(ActorRepository actorRepository, PlayerRepository playerRepository, SessionRepository sessionRepository)
        {
            this.actorRepository = actorRepository;
            this.playerRepository = playerRepository;
            this.sessionRepository = sessionRepository;
        }

        public virtual IObservable<Actor> Execute(string name, Skin bodySkin, Skin headSkin)
        {
            return playerRepository.Get()
                .ReturnOrDefault(player =>
                    {
                        var actor = new Actor(player.id, name, new ActorSkin(bodySkin, headSkin), player);
                        sessionRepository.Save(new Session(player.id, actor.id));
                        return actorRepository.Save(actor).Select(_ => actor);
                    },
                    new Actor().ToObservableDummy());
        }
    }
}