using System;
using Modules.Humor.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Humor.UnityDelivery
{
    public class UnityHumorBarView : MonoBehaviour, HumorBarView
    {
        public event Action OnViewEnable = () => { };
        public event Action OnViewDisable = () => { };

        [SerializeField] private Slider bar;

        public void HumorChange(int HumorLevel, int LastHumorChange)
        {
            var maxHumor = 100; //TODO: Sacar esto de alguna configuracion.
            float actualValue = (float)HumorLevel / maxHumor;
            bar.value = actualValue;
        }

        private void Awake()
        {
            HumorModuleProvider.ProvidePresenterFor(this);
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
