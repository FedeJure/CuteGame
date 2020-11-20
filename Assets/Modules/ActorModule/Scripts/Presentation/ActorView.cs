using System;

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
        void InitActor(string actorName, string skinBodySkinId, string skinHeadSkinId);
    }
}