using System;
using Modules.ActorModule.Scripts.Core.Domain.Action;
using Modules.ActorModule.Scripts.Presentation;
using Modules.ActorModule.Scripts.Presentation.Events;
using NSubstitute;
using NUnit.Framework;

namespace Modules.ActorModule.Tests.Presentation
{
    public class TouchHelperPresenterTest
    {
        private TouchHelperView view;
        private ProcessInteraction _processInteraction;
        [SetUp]
        public void SetUp()
        {
            view = Substitute.For<TouchHelperView>();
            _processInteraction = Substitute.For<ProcessInteraction>();
            
            new TouchHelperPresenter(view, _processInteraction);
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
            _processInteraction.Received(1).Execute(Arg.Any<ActorInteraction>());
        }
    }
}