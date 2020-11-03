using System;
using Modules.Actor.Scripts.Presentation.Events;

namespace Modules.Actor.Scripts.Presentation
{
    public interface TouchHelperView
    {
        event Action OnViewEnabled;
        event Action<ActorInteraction> OnActorInteraction;
    }
}