using System;
using System.Collections.Generic;
using Modules.Common;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Sources.Scripts
{
    public class CloseButton : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject content;
        [SerializeField] private Button button;
    
        private int closeKey = Animator.StringToHash("close");

        private List<IDisposable> disposer = new List<IDisposable>();
        private void Start()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnEnable()
        {
            animator.GetBehaviour<ObservableStateMachineTrigger>()
                .OnStateMachineExitAsObservable()
                .AsUnitObservable()
                .Do(_ => content.SetActive(false))
                .Subscribe()
                .AddTo(disposer);
        }

        private void OnDisable()
        {
            disposer.DisposeAll();
        }

        private void OnClick()
        {
            animator.SetTrigger(closeKey);
        }
    }
}
