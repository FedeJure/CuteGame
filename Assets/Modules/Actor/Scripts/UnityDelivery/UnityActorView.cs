﻿using System;
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

        private void Awake()
        {
            ModuleProvider.ProvidePresenterFor(this);
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
    }
}