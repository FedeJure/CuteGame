using System;
using UnityEngine;

namespace Modules.Actor.Scripts.Presentation
{
    public interface ActorView
    {
        event Action OnViewEnable;
        event Action OnViewDisable;
        void ShowCaredFeedback();
        void ShowNotHappyFeedback();
    }
}