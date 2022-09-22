using System;
using Modules.ActorModule.Scripts.Core.Domain;
using UnityEngine;

namespace Modules.ActorModule.Scripts.Presentation
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
        void ShowLeftTickleFeedback();
        void ShowRightTickleFeedback();
        void ShowMiddleConsentEvent();
        void SetActorInteractable(bool interactable);
        void SetHeadSkin(Skin headSkin, Color? overridedColor = null);
        void SetBodySkin(Skin bodySkin, Color? overridedColor = null);
        void RestoreAnimator();
    }
}