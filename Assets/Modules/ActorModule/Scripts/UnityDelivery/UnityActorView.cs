using System;
using Modules.ActorModule.Scripts.Presentation;
using Modules.ActorModule.Scripts.UnityDelivery.Skin;
using UnityEngine;

namespace Modules.ActorModule.Scripts.UnityDelivery
{
    public class UnityActorView : MonoBehaviour, ActorView
    {
        public event Action OnViewEnable = () => { };
        public event Action OnViewDisable = () => { };

        [SerializeField] Animator animator;
        [SerializeField] private GameObject interactables;
        [SerializeField] private SkinnedMeshRenderer meshRenderer;
        [SerializeField] private ActorSkinConfig skinConfig;

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

        public void SetActorInteractable(bool interactable)
        {
            interactables.SetActive(interactable);
        }

        public void InitActor(string actorName, string skinBodySkinId, string skinHeadSkinId)
        {
            var mats = new Material[2];
            skinConfig.skins.ForEach(skin =>
            {
                if (skin.key == skinHeadSkinId)
                {
                    mats[0] = skin.material;
                }
                if (skin.key == skinBodySkinId)
                {
                    mats[1] = skin.material;
                }
            });
            meshRenderer.sharedMaterials = mats;
        }
    }
}
