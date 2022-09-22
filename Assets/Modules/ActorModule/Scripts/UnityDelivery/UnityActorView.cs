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

        public void SetHeadSkin(Core.Domain.Skin headSkin, Color? overridedColor = null)
        {
            skinConfig.skins.ForEach(skin =>
            {
                if (skin.key != headSkin.key || skin.type != SkinType.Head) return;
                creamMesh.sharedMaterial = skin.material;
                SetColor(headSkin, creamMesh, overridedColor);
            });
        }
        public void SetBodySkin(Core.Domain.Skin bodySkin, Color? overridedColor = null)
        {
            skinConfig.skins.ForEach(skin =>
            {
                if (skin.key != bodySkin.key || skin.type != SkinType.Body) return;
                bodyMesh.material = skin.material;
                SetColor(bodySkin, bodyMesh, overridedColor);
            });
        }

        private void SetColor(Core.Domain.Skin skin, Renderer mesh, Color? overridedColor)
        {
            if (!skin.colorOverrided) return;
            var color = overridedColor ?? skin.color;
            mesh.material.SetColor("_Color", color);
            mesh.material.SetColor("_HColor", new Color(color.r + 0.1f, color.g + 0.1f, color.b + 0.1f));
            mesh.material.SetColor("_SColor", new Color(0, 0, 0, 0.5f));
        }

        public void RestoreAnimator()
        {
            animator.runtimeAnimatorController = animatorController;
        }
    }
}
