using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class SimpleMenuButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] GameObject menu;
    [SerializeField] private Animator animator;

    private int openKey = Animator.StringToHash("open");
    private int closeKey = Animator.StringToHash("close");

    private List<IDisposable> disposer = new List<IDisposable>();
    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnEnable()
    {
        menu.SetActive(false);
        animator.GetBehaviour<ObservableStateMachineTrigger>()
            .OnStateMachineExitAsObservable()
            .AsUnitObservable()
            .Do(_ => menu.SetActive(false))
            .Subscribe()
            .AddTo(disposer);
    }

    private void OnDisable()
    {
        disposer.ForEach(d => d.Dispose());
    }

    public void OnClick()
    {
        Debug.LogWarning("Click");
        if (menu.activeSelf)
        {
            animator.SetTrigger(closeKey);
            return;
        }
        menu.SetActive(true);
        animator.SetTrigger(openKey);
    }
}
