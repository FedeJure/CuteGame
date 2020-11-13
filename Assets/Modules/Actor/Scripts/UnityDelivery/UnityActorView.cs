using System;
using Modules.Actor.Scripts.Presentation;
using UnityEngine;

namespace Modules.Actor.Scripts.UnityDelivery
{
    public class UnityActorView : MonoBehaviour, ActorView
    {
        public event Action OnViewEnable = () => { };
        public event Action OnViewDisable = () => { };

        [SerializeField] Animator animator;

        readonly int leftCaredKey = Animator.StringToHash("leftCaress");
        readonly int rigthCaredKey = Animator.StringToHash("rigthCaress");
        readonly int happyKey = Animator.StringToHash("happy");
        readonly int notHappyKey = Animator.StringToHash("notHappy");
        readonly int normalHumorKey = Animator.StringToHash("normalHumor");
        readonly int notHappyHumorKey = Animator.StringToHash("notHappyHumor");
        readonly int happyHumorKey = Animator.StringToHash("happyHumor");
        readonly int leftTickleKey = Animator.StringToHash("leftTickle");
        readonly int rightTickleKey = Animator.StringToHash("rightTickle");
        readonly int middleConsentKey = Animator.StringToHash("middleConsent");

        private void Awake()
        {
            ActorModuleProvider.ProvidePresenterFor(this);
        }

        private void OnEnable()
        {
            OnViewEnable();
        }

        private void OnDisable()
        {
            OnViewDisable();
        }

        public void ShowNotHappyFeedback()
        {
            animator.SetTrigger(notHappyKey);
        }

        public void ShowHappyFeedback()
        {
            animator.SetTrigger(happyKey);
        }

        public void ShowRigthCaredFeedback()
        {
            animator.SetTrigger(rigthCaredKey);
        }

        public void ShowLeftCaredFeedback()
        {
            animator.SetTrigger(leftCaredKey);
        }

        public void ShowNormalIdle()
        {
            animator.SetTrigger(normalHumorKey);
        }

        public void ShowHappyIdle()
        {
            animator.SetTrigger(happyHumorKey);
        }

        public void ShowNotHappyIdle()
        {
            animator.SetTrigger(notHappyHumorKey);

        }

        public void ShowLeftTickleFeedback()
        {
            animator.SetTrigger(leftTickleKey);
        }

        public void ShowRightTickleFeedback()
        {
            animator.SetTrigger(rightTickleKey);
        }

        public void ShowMiddleConsentEvent()
        {
            animator.SetTrigger(middleConsentKey);
        }
    }
}
