using System;
using Modules.Actor.Scripts.Core.Domain.Repositories;
using Modules.Common;

namespace Modules.Actor.Scripts.Core.Domain.Action
{
    public class CreateNewActor
    {
        private readonly ActorRepository actorRepository;
        private readonly PlayerRepository playerRepository;

        public CreateNewActor(ActorRepository actorRepository, PlayerRepository playerRepository)
        {
            this.actorRepository = actorRepository;
            this.playerRepository = playerRepository;
        }

        public IObservable<Actor> Execute(string name, string bodySkinId, string headSkinId)
        {
            return playerRepository.Get()
                .ReturnOrDefault(player =>
                    {
                        var actor = new Actor(player.id, name, new ActorSkin(bodySkinId, headSkinId));
                        actorRepository.Save(actor);
                        return actor.ToObservableDummy();
                    },
                    new Actor(0,"", new ActorSkin("","")).ToObservableDummy());
        }
    }
}