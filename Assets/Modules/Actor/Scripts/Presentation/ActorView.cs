using System;
using UnityEngine;

namespace Modules.Actor.Scripts.Presentation
{
    public interface ActorView
    {
        event Action OnViewEnable;
        event Action OnViewDisable;
        void ShowNotHappyFeedback();
        void ShowHappyFeedback();
        void ShowRigthCaredFeedback();
        void ShowLeftCaredFeedback();
        void ShowNormalIdle();
        void ShowHappyIdle();
        void ShowNotHappyIdle();
    }
}