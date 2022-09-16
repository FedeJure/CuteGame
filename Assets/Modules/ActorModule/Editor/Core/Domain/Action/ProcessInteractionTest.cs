using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Action;
using Modules.ActorModule.Scripts.Core.Domain.Events;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.ActorModule.Scripts.Presentation.Events;
using Modules.Common;
using Modules.MainGame.Scripts.Core.Domain;
using NSubstitute;
using NUnit.Framework;

namespace Modules.ActorModule.Tests.Core.Domain.Action
{
    public class ProcessInteractionTest
    {
        private int MAX_HUMOR = 100;
        private string ACTOR_ID = "ID";
        ProcessInteraction action;
        private EventBus eventBus;
        private HumorStateRepository humorStateRepository;
        private HumorStateService humorStateService;
        private SessionRepository sessionRepository;

        [SetUp]
        public void SetUp()
        {
            eventBus = Substitute.For<EventBus>();
            humorStateRepository = Substitute.For<HumorStateRepository>();
            humorStateService = Substitute.For<HumorStateService>();
            sessionRepository = Substitute.For<SessionRepository>();

            sessionRepository.Get().Returns(new Session("player id", ACTOR_ID).ToMaybe());
            humorStateRepository.Get(ACTOR_ID).Returns(new Maybe<HumorState>(new HumorState(50, 0, Humor.Normal, MAX_HUMOR)));
            action = new ProcessInteraction(eventBus, humorStateRepository, humorStateService, sessionRepository);
        }
        
        [Test]
        public void save_new_state()
        {
            GivenInteractionDecreaseHumor();
            WhenActionCalled();
            ThenNewStateSaved();
        }

        [Test]
        public void emit_happy_on_increase_humor()
        {
            GivenInteractionIncreaseHumor();
            WhenActionCalled();
            ThenHappyEventEmited();
        }

        [Test]
        public void emit_not_happy_on_increase_humor()
        {
            GivenInteractionDecreaseHumor();
            WhenActionCalled();
            ThenNotHappyEventEmited();
        }

        [Test]
        public void not_emit_event_on_no_humor_changes()
        {
            GivenInteractionNoChangeHumor();
            WhenActionCalled();
            ThenNoEventEmited();   
        }
        
        [Test]
        public void emit_humor_changes_event_on_humor_changes()
        {
            GivenInteractionDecreaseHumor();
            WhenActionCalled();
            ThenHumorChangesEventEmited();   
        }

        private void ThenHumorChangesEventEmited()
        {
            eventBus.Received(1).EmitEvent<HumorChangesEvent>();
        }

        private void GivenInteractionNoChangeHumor()
        {
            humorStateService.ReceiveInteraction(Arg.Any<ActorInteraction>()).Returns(new HumorState(100, 0, Humor.Normal,MAX_HUMOR));
        }

        private void ThenNoEventEmited()
        {
            eventBus.Received(0).EmitEvent<HumorChangesEvent>();
        }

        private void GivenInteractionDecreaseHumor()
        {
            humorStateService.ReceiveInteraction(Arg.Any<ActorInteraction>()).Returns(new HumorState(100, -1, Humor.Normal,MAX_HUMOR));
        }

        private void ThenNotHappyEventEmited()
        {
            eventBus.Received(1).EmitEvent<NotHappyEvent>();
        }

        private void GivenInteractionIncreaseHumor()
        {
            humorStateService.ReceiveInteraction(Arg.Any<ActorInteraction>()).Returns(new HumorState(100, 1, Humor.Normal,MAX_HUMOR));
        }

        private void WhenActionCalled()
        {
            action.Execute(ActorInteraction.Consent);
        }

        private void ThenHappyEventEmited()
        {
            eventBus.Received(1).EmitEvent<HappyEvent>();
        }

        private void ThenNewStateSaved()
        {
            humorStateRepository.Received(1).Save(Arg.Any<HumorState>(), ACTOR_ID);
        }
    }
}