using System;
using Modules.Actor.Scripts.Core.Domain;
using Modules.Actor.Scripts.Core.Domain.Action;
using Modules.Actor.Scripts.Core.Domain.Repositories;
using Modules.Common;
using NSubstitute;
using NUnit.Framework;
using UniRx;

namespace Modules.Actor.Tests.Core.Domain.Action
{
    public class CreateNewActorTest
    {
        private const string ACTOR_NAME = "name";
        private ActorRepository actorRepository;
        private PlayerRepository playerRepository;
        private CreateNewActor action;

        [SetUp]
        public void SetUp()
        {
            actorRepository = Substitute.For<ActorRepository>();
            playerRepository = Substitute.For<PlayerRepository>();
            action = new CreateNewActor(actorRepository, playerRepository);   
        }
    
        [Test]
        public void creates_new_actor_with_player_available()
        {
            GivenPlayerAvailable();
            var result = WhenCallAction();
            ThenActorSavedOnRepository();
            ThenActorReturned(result);
        }

        private void GivenPlayerAvailable()
        {
            playerRepository.Get().Returns(new Player(1).ToMaybe());
        }

        private IObservable<Scripts.Core.Domain.Actor> WhenCallAction()
        {
            return action.Execute(ACTOR_NAME, "skin1", "skin1");
        }

        private void ThenActorSavedOnRepository()
        {
            actorRepository.Received(1).Save(Arg.Any<Scripts.Core.Domain.Actor>());
        }

        private void ThenActorReturned(IObservable<Scripts.Core.Domain.Actor> result)
        {
            result.Do(actor => Assert.AreEqual(actor.name, ACTOR_NAME)).Subscribe();
        }
    }
}