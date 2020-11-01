using System;
using Modules.Actor.Scripts.Presentation;
using UnityEngine;

namespace Modules.Actor.Scripts.UnityDelivery
{
    public class UnityActorView : MonoBehaviour, ActorView
    {
        public event Action OnViewEnable;
        public event Action OnViewDisable;

        private void Awake()
        {
            ModuleProvider.ProvidePresenterFor(this);
        }

        public void ShowCaredFeedback()
        {
            Debug.LogWarning("Show cared Feedback");
        }

        public void ShowNotHappyFeedback()
        {
            Debug.LogWarning("Show not happy Feedback");
        }

        public void ShowHappyFeedback()
        {
            Debug.LogWarning("Show happy Feedback");
        }
    }
}
