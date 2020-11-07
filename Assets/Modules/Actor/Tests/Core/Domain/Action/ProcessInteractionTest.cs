using System;
using Modules.Actor.Scripts.Core;
using Modules.Actor.Scripts.Core.Domain;
using Modules.Actor.Scripts.Core.Domain.Action;
using Modules.Actor.Scripts.Core.Domain.Events;
using Modules.Actor.Scripts.Infrastructure;
using Modules.Actor.Scripts.Presentation.Events;
using NSubstitute;
using NUnit.Framework;

namespace Modules.Actor.Tests.Core.Domain.Action
{
    public class ProcessInteractionTest
    {
        ProcessInteraction action;
        private EventBus eventBus;
        private HumorStateRepository humorStateRepository;
        private HumorStateService humorStateService;

        [SetUp]
        public void SetUp()
        {
            eventBus = Substitute.For<EventBus>();
            humorStateRepository = Substitute.For<HumorStateRepository>();
            humorStateService = Substitute.For<HumorStateService>();

            humorStateRepository.Get().Returns(new HumorState(50, 0));
            action = new ProcessInteraction(eventBus, humorStateRepository, humorStateService);
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
            humorStateService.ReceiveInteraction(Arg.Any<ActorInteraction>()).Returns(new HumorState(100, 0));
        }

        private void ThenNoEventEmited()
        {
            eventBus.Received(0).EmitEvent<Object>();
        }

        private void GivenInteractionDecreaseHumor()
        {
            humorStateService.ReceiveInteraction(Arg.Any<ActorInteraction>()).Returns(new HumorState(100, -1));
        }

        private void ThenNotHappyEventEmited()
        {
            eventBus.Received(1).EmitEvent<NotHappyEvent>();
        }

        private void GivenInteractionIncreaseHumor()
        {
            humorStateService.ReceiveInteraction(Arg.Any<ActorInteraction>()).Returns(new HumorState(100, 1));
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
            humorStateRepository.Received(1).Save(Arg.Any<HumorState>());
        }
    }
}