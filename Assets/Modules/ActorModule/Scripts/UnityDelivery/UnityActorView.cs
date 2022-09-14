using System;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Presentation;
using Modules.ActorModule.Scripts.UnityDelivery.Skin;
using Modules.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modules.ActorModule.Scripts.UnityDelivery
{
    public class UnityActorView : MonoBehaviour, ActorView
    {
        public event Action OnViewEnable = () => { };
        public event Action OnViewDisable = () => { };

        [SerializeField] Animator animator;
        [SerializeField] private GameObject interactables;
        [SerializeField] private Renderer bodyMesh;
        [SerializeField] private Renderer creamMesh;
        [SerializeField] private ActorSkinConfig skinConfig;
        [SerializeField] private RuntimeAnimatorController animatorController;

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
            
            ActorComponentsRepository.SetAnimator(animator);
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

        public void SetHeadSkin(Core.Domain.Skin headSkin)
        {
            skinConfig.skins.ForEach(skin =>
            {
                if (skin.key != headSkin.key || skin.type != SkinType.Head) return;
                creamMesh.sharedMaterial = skin.material;
                SetColor(headSkin.color, creamMesh);
            });
        }
        public void SetBodySkin(Core.Domain.Skin bodySkin)
        {
            skinConfig.skins.ForEach(skin =>
            {
                if (skin.key != bodySkin.key || skin.type != SkinType.Body) return;
                bodyMesh.material = skin.material;
                SetColor(bodySkin.color, bodyMesh);
            });
        }

        private void SetColor(Color? color, Renderer mesh)
        {
            if (!color.HasValue) return;
            mesh.material.SetColor("_Color", color.Value);
            mesh.material.SetColor("_HColor", new Color(color.Value.r + 0.1f, color.Value.g + 0.1f, color.Value.b + 0.1f));
            mesh.material.SetColor("_SColor", new Color(color.Value.r - 0.25f, color.Value.g - 0.25f, color.Value.b - 0.25f));
        }

        public void RestoreAnimator()
        {
            animator.runtimeAnimatorController = animatorController;
        }
    }
}
