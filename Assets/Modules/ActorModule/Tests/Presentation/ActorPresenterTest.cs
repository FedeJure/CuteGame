using System;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Action;
using Modules.ActorModule.Scripts.Core.Domain.Events;
using Modules.ActorModule.Scripts.Presentation;
using Modules.Common;
using NSubstitute;
using NUnit.Framework;
using UniRx;

namespace Modules.ActorModule.Tests.Presentation
{
    public class ActorPresenterTest
    {
        private const int MAX_HUMOR = 100;
        private ActorView view;
        private EventBus eventBus;
        private RetrieveActorHumor retrieveActorHumor;

        private ISubject<Unit> leftCaressEvent = new Subject<Unit>();
        private ISubject<Unit> rigthCaressEvent = new Subject<Unit>();
        private ISubject<Unit> notHappyEvent = new Subject<Unit>();
        private ISubject<Unit> happyEvent = new Subject<Unit>();
        private GlobalEventBus globalEventBus;

        [SetUp]
        public void SetUp()
        {
            view = Substitute.For<ActorView>();
            eventBus = Substitute.For<EventBus>();
            retrieveActorHumor = Substitute.For<RetrieveActorHumor>();
            globalEventBus = Substitute.For<GlobalEventBus>();

            retrieveActorHumor.Execute().Returns(new Maybe<HumorState>(new HumorState(50, 1, Humor.Happy, MAX_HUMOR)));
            eventBus.OnEvent<LeftCaressInteractionEvent>().Returns(leftCaressEvent);
            eventBus.OnEvent<RigthCaressInteractionEvent>().Returns(rigthCaressEvent);
            eventBus.OnEvent<NotHappyEvent>().Returns(notHappyEvent);
            eventBus.OnEvent<HappyEvent>().Returns(happyEvent);
            new ActorPresenter(view, eventBus, globalEventBus, retrieveActorHumor);
        }

        [Test]
        public void OnNotHappyEvent()
        {
            GivenViewIsPresented();
            WhenNotHappyEventRaised();
            ThenShowNotHappyFeedback();
        }
        
        [Test]
        public void OnLeftCaressActorEvent()
        {
            GivenViewIsPresented();
            WhenLeftCaressEventRaised();
            ThenLeftCaressFeedbackShowed();
        }
        
        [Test]
        public void OnRigthCaressActorEvent()
        {
            GivenViewIsPresented();
            WhenRigthCaressEventRaised();
            ThenRigthCaressFeedbackShowed();
        }

        [Test]
        public void OnHappyEvent()
        {
            GivenViewIsPresented();
            WhenHappyEventRaised();
            ThenHappyFeedbackShowed();
        }

        private void GivenViewIsPresented()
        {
            view.OnViewEnable += Raise.Event<Action>();
        }

        private void WhenNotHappyEventRaised()
        {
            notHappyEvent.OnNext(Unit.Default);
        }

        private void ThenLeftCaressFeedbackShowed()
        {
            view.Received(1).ShowLeftCaredFeedback();
        }

        private void WhenLeftCaressEventRaised()
        {
            leftCaressEvent.OnNext(Unit.Default);
        }
        
        private void ThenRigthCaressFeedbackShowed()
        {
            view.Received(1).ShowRigthCaredFeedback();
        }

        private void WhenRigthCaressEventRaised()
        {
            rigthCaressEvent.OnNext(Unit.Default);
        }


        private void ThenShowNotHappyFeedback()
        {
            view.Received(1).ShowNotHappyFeedback();
        }

        private void WhenHappyEventRaised()
        {
            happyEvent.OnNext(Unit.Default);
        }

        private void ThenHappyFeedbackShowed()
        {
            view.Received(1).ShowHappyFeedback();
        }
    }
}
