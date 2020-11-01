using System;
using Modules.Actor.Scripts.Core;
using Modules.Actor.Scripts.Presentation;
using Modules.Actor.Scripts.Presentation.Events;
using NSubstitute;
using NUnit.Framework;

namespace Modules.Actor.Tests.Presentation
{
    public class TouchHelperPresenterTest
    {
        private TouchHelperView view;
        private EventBus eventBus;
        [SetUp]
        public void SetUp()
        {
            view = Substitute.For<TouchHelperView>();
            eventBus = Substitute.For<EventBus>();
            
            new TouchHelperPresenter(view, eventBus);
        }

        [Test]
        public void ProcessTouchAction()
        {
            GivenTouchHelperWasPresented();
            WhenTouchActionRaised();
            ThenProcessTouchActionExecuted();
        }

        private void GivenTouchHelperWasPresented()
        {
            view.OnEnabled += Raise.Event<Action>();
        }

        private void WhenTouchActionRaised()
        {
            view.OnSwipeAction += Raise.Event<Action<SwipeAction>>(new SwipeAction());
        }

        private void ThenProcessTouchActionExecuted()
        {
        }
    }
}