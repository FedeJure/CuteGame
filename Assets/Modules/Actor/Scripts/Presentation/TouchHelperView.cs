using System;
using Modules.Actor.Scripts.Presentation.Events;

namespace Modules.Actor.Scripts.Presentation
{
    public interface TouchHelperView
    {
        event Action OnEnabled;
        event Action<SwipeAction> OnSwipeAction;
    }
}