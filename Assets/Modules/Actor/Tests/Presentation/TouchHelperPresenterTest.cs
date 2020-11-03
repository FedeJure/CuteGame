using System;
using Modules.Actor.Scripts.Core.Domain.Action;
using Modules.Actor.Scripts.Presentation;
using Modules.Actor.Scripts.Presentation.Events;
using NSubstitute;
using NUnit.Framework;

namespace Modules.Actor.Tests.Presentation
{
    public class TouchHelperPresenterTest
    {
        private TouchHelperView view;
        private ProcessDirectionAction processDirectionAction;
        [SetUp]
        public void SetUp()
        {
            view = Substitute.For<TouchHelperView>();
            processDirectionAction = Substitute.For<ProcessDirectionAction>();
            
            new TouchHelperPresenter(view, processDirectionAction);
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
            view.OnViewEnabled += Raise.Event<Action>();
        }

        private void WhenTouchActionRaised()
        {
            view.OnActorInteraction += Raise.Event<Action<ActorInteraction>>(Arg.Any<ActorInteraction>());
        }

        private void ThenProcessTouchActionExecuted()
        {
            processDirectionAction.Received(1).Execute(Arg.Any<ActorInteraction>());
        }
    }
}