using System;
using System.Collections.Generic;
using Modules.Actor.Scripts.Presentation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Actor.Scripts.UnityDelivery
{
    public class UnityHumorBarView : MonoBehaviour, HumorBarView
    {
        public event Action OnViewEnable = () => { };
        public event Action OnViewDisable = () => { };

        [SerializeField] private Slider bar;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Color incrementColor;
        [SerializeField] private Color decrementColor;
        
        List<IDisposable> disposer = new List<IDisposable>();
        

        private Vector3 textPosition;
        private Vector3 textScale;

        private void Awake()
        {
            ActorModuleProvider.ProvidePresenterFor(this);
        }

        private void Start()
        {
            textPosition = text.transform.position;
            textScale = text.transform.localScale;
        }

        private void OnEnable()
        {
            OnViewEnable();
            text.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            OnViewDisable();
            disposer.ForEach(d => d.Dispose());
        }

        public void HumorChange(int HumorLevel, int LastHumorChange, int maxHumor)
        {
            SetValue(HumorLevel, maxHumor);
            bool increment = LastHumorChange > 0;
            
            text.text = increment ? $"+{LastHumorChange}" : LastHumorChange.ToString();
            text.color = increment ? incrementColor : decrementColor;
            LeanTween.scale(bar.gameObject, bar.transform.localScale * 1.08f, 0.2f)
                .setLoopPingPong(1)
                .setEaseInOutBounce();
            TextAnimation();
        }

        private void TextAnimation()
        {
            var scale = text.transform.localScale;
            var position = text.transform.position;
            text.transform.localScale = Vector3.zero;
            text.gameObject.SetActive(true);
            LeanTween.scale(text.gameObject, textScale * 3f, 0.4f)
                .setLoopPingPong(1)
                .setOnComplete(() =>
                {
                    text.transform.localScale = textScale;
                    text.gameObject.SetActive(false);
                });

            LeanTween.move(text.gameObject, textPosition + Vector3.forward / 5, 0.4f)
                .setOnComplete(() =>
                {
                    text.transform.position = textPosition;
                    text.gameObject.SetActive(false);
                });
        }

        public void InitView(int humor, int maxHumor)
        {
            SetValue(humor, maxHumor);
        }

        void SetValue(int humor, int maxHumor)
        {
            float actualValue = (float)humor / maxHumor;
            bar.value = actualValue;
        }
    }
}
