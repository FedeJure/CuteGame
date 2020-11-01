﻿using System;
using Modules.Actor.Scripts.Core;
using Modules.Actor.Scripts.Presentation;
using NSubstitute;
using NUnit.Framework;
using UniRx;

namespace Modules.Actor.Tests.Presentation
{
    public class ActorPresenterTest
    {
        private ActorView view;
        private EventBus eventBus;

        private ISubject<Unit> caressEvent = new Subject<Unit>();
        private ISubject<Unit> notHappyEvent = new Subject<Unit>();
        
        [SetUp]
        public void SetUp()
        {
            view = Substitute.For<ActorView>();
            eventBus = Substitute.For<EventBus>();

            eventBus.OnCaressEvent().Returns(caressEvent);
            eventBus.OnNotHappyEvent().Returns(notHappyEvent);
            new ActorPresenter(view, eventBus);
        }

        [Test]
        public void OnNotHappyEvent()
        {
            GivenViewIsPresented();
            WhenNotHappyEventRaised();
            ThenShowNotHappyFeedback();
        }
        
        [Test]
        public void OnCaressActorEvent()
        {
            GivenViewIsPresented();
            WhenCaressEventRaised();
            ThenCaressFeedbackShowed();
        }

        private void GivenViewIsPresented()
        {
            view.OnViewEnable += Raise.Event<Action>();
        }

        private void WhenNotHappyEventRaised()
        {
            notHappyEvent.OnNext(Unit.Default);
        }

        private void ThenCaressFeedbackShowed()
        {
            view.Received(1).ShowCaredFeedback();
        }

        private void WhenCaressEventRaised()
        {
            caressEvent.OnNext(Unit.Default);
        }

        private void ThenShowNotHappyFeedback()
        {
            view.Received(1).ShowNotHappyFeedback();
        }
    }
}
