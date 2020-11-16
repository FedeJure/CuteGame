using System;
using Modules.ActorModule.Scripts.Presentation.Events;

namespace Modules.ActorModule.Scripts.Presentation
{
    public interface TouchHelperView
    {
        event Action OnViewEnabled;
        event Action<ActorInteraction> OnActorInteraction;
    }
}