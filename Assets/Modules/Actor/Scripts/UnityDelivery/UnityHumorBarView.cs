using System;
using Modules.Actor.Scripts.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Actor.Scripts.UnityDelivery
{
    public class UnityHumorBarView : MonoBehaviour, HumorBarView
    {
        public event Action OnViewEnable = () => { };
        public event Action OnViewDisable = () => { };

        [SerializeField] private Slider bar;

        public void HumorChange(int HumorLevel, int LastHumorChange, int maxHumor)
        {
            float actualValue = (float)HumorLevel / maxHumor;
            bar.value = actualValue;
        }

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
    }
}
