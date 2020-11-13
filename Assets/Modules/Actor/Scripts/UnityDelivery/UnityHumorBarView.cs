using System;
using System.Collections.Generic;
using Modules.Actor.Scripts.Presentation;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Actor.Scripts.UnityDelivery
{
    public class UnityHumorBarView : MonoBehaviour, HumorBarView
    {
        public event Action OnViewEnable = () => { };
        public event Action OnViewDisable = () => { };

        [SerializeField] private Slider bar;
        [SerializeField] private Animator animator;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Color incrementColor;
        [SerializeField] private Color decrementColor;
        
        List<IDisposable> disposer = new List<IDisposable>();
        

        private int valueChangeKey = Animator.StringToHash("valueChange");

        private void Awake()
        {
            ActorModuleProvider.ProvidePresenterFor(this);
        }

        private void OnEnable()
        {
            OnViewEnable();
            text.gameObject.SetActive(false);
            animator.GetBehaviour<ObservableStateMachineTrigger>()
                .OnStateMachineExitAsObservable()
                .Do(_ => text.gameObject.SetActive(false))
                .Subscribe()
                .AddTo(disposer);
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
            text.gameObject.SetActive(true);
            text.text = increment ? $"+{LastHumorChange}" : LastHumorChange.ToString();
            text.color = increment ? incrementColor : decrementColor;
            animator.SetTrigger(valueChangeKey);
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
