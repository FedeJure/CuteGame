using System;
using Modules.ActorModule.Scripts.Core.Domain.Action;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.Common;
using Modules.MainGame.Scripts.Core.Domain;
using Modules.PlayerModule.Scripts.Core.Domain;
using Modules.PlayerModule.Scripts.Core.Domain.Repositories;
using NSubstitute;
using NUnit.Framework;
using UniRx;

namespace Modules.ActorModule.Tests.Core.Domain.Action
{
    public class CreateNewActorTest
    {
        private const string ACTOR_NAME = "name";
        private ActorRepository actorRepository;
        private PlayerRepository playerRepository;
        private CreateNewActor action;
        private SessionRepository sessionRepository;

        [SetUp]
        public void SetUp()
        {
            actorRepository = Substitute.For<ActorRepository>();
            playerRepository = Substitute.For<PlayerRepository>();
            sessionRepository = Substitute.For<SessionRepository>();
            action = new CreateNewActor(actorRepository, playerRepository, sessionRepository);   
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
            playerRepository.Get().Returns(new Player("Test id").ToMaybe());
        }

        private IObservable<ActorModule.Scripts.Core.Domain.Actor> WhenCallAction()
        {
            return action.Execute(ACTOR_NAME, "skin1", "skin1");
        }

        private void ThenActorSavedOnRepository()
        {
            actorRepository.Received(1).Save(Arg.Any<ActorModule.Scripts.Core.Domain.Actor>());
        }

        private void ThenActorReturned(IObservable<ActorModule.Scripts.Core.Domain.Actor> result)
        {
            result.Do(actor => Assert.AreEqual(actor.name, ACTOR_NAME)).Subscribe();
        }
    }
}