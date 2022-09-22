using System;
using System.Collections.Generic;
using Modules.ActorModule.Scripts.Presentation;
using Modules.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.ActorModule.Scripts.UnityDelivery
{
    public class UnityHumorBarView : MonoBehaviour, HumorBarView
    {
        public event Action OnViewEnable = () => { };
        public event Action OnViewDisable = () => { };

        [SerializeField] private Slider bar;
        [SerializeField] private Image background;
        [SerializeField] private Color incrementColor;
        [SerializeField] private Color decrementColor;
        [SerializeField] private Color defaultColor;

        
        List<IDisposable> disposer = new List<IDisposable>();
        

        private Vector3 textPosition;
        private Vector3 textScale;

        private void Awake()
        {
            ActorModuleProvider.ProvidePresenterFor(this);
            background.color = defaultColor;
        }

        private void OnEnable()
        {
            OnViewEnable();
        }

        private void OnDisable()
        {
            OnViewDisable();
            disposer.DisposeAll();
        }

        public void HumorChange(float HumorLevel, float LastHumorChange, float maxHumor)
        {
            SetValue(HumorLevel, maxHumor);
            bar.transform.localScale = new Vector3(1,1,1);
            background.color = defaultColor;
            bool increment = LastHumorChange > 0;

            LeanTween.color(background.rectTransform, increment ? incrementColor : decrementColor, 0.2f)
                .setLoopPingPong(1);
            
            LeanTween.scale(bar.gameObject, bar.transform.localScale * 1.08f, 0.2f)
                .setLoopPingPong(1)
                .setEaseInOutBounce();
        }

        public void InitView(float humor, float maxHumor)
        {
            SetValue(humor, maxHumor);
        }

        void SetValue(float humor, float maxHumor)
        {
            float actualValue = humor / maxHumor;
            bar.value = actualValue;
        }
    }
}
