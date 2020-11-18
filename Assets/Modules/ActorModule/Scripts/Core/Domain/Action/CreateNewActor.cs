using System;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;
using Modules.PlayerModule.Scripts.Core.Domain;
using Modules.PlayerModule.Scripts.Core.Domain.Repositories;

namespace Modules.ActorModule.Scripts.Core.Domain.Action
{
    public class CreateNewActor
    {
        private readonly ActorRepository actorRepository;
        private readonly PlayerRepository playerRepository;

        public CreateNewActor() { }
        public CreateNewActor(ActorRepository actorRepository, PlayerRepository playerRepository)
        {
            this.actorRepository = actorRepository;
            this.playerRepository = playerRepository;
        }

        public virtual IObservable<Actor> Execute(string name, string bodySkinId, string headSkinId)
        {
            return playerRepository.Get()
                .ReturnOrDefault(player =>
                    {
                        var actor = new Actor(player.id, name, new ActorSkin(bodySkinId, headSkinId), player);
                        actorRepository.Save(actor);
                        return actor.ToObservableDummy();
                    },
                    new Actor(0,"", new ActorSkin("",""), new Player(0)).ToObservableDummy());
        }
    }
}