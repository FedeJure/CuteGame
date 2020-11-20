using System;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;
using Modules.MainGame.Scripts.Core.Domain;
using Modules.PlayerModule.Scripts.Core.Domain;
using Modules.PlayerModule.Scripts.Core.Domain.Repositories;

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

        public virtual IObservable<Actor> Execute(string name, string bodySkinId, string headSkinId)
        {
            return playerRepository.Get()
                .ReturnOrDefault(player =>
                    {
                        var actor = new Actor(player.id, name, new ActorSkin(bodySkinId, headSkinId), player);
                        actorRepository.Save(actor);
                        sessionRepository.Save(new Session(player.id, actor.id));
                        return actor.ToObservableDummy();
                    },
                    new Actor(0,"", new ActorSkin("",""), new Player(0)).ToObservableDummy());
        }
    }
}