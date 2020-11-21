using System;
using System.Collections.Generic;
using Modules.Common;
using Modules.Shop.Scripts.Presentation;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Shop.Scripts.UnityDelivery
{
    public class UnityShopView : MonoBehaviour, ShopView
    {
        public event Action OnViewEnable = () => { };
        public event Action OnCloseClicked = () => { };

        [SerializeField] private GameObject content;
        [SerializeField] private Button closeButton;
        
        List<IDisposable> disposer = new List<IDisposable>();

        private void Awake()
        {
            ShopModuleProvider.ProvidePresenterFor(this);
            
            closeButton.onClick.AddListener(OnCloseClick);
        }

        private void OnCloseClick()
        {
            OnCloseClicked();
        }

        private void OnEnable()
        {
            OnViewEnable();
        }

        public void ShowOpenAnimation()
        {
            content.transform.localScale = Vector3.zero;
            LeanTween.scale(content, new Vector3(1, 1, 1), 0.2f)
                .setEaseInQuad();
        }

        public void Close()
        {
            LeanTween.scale(content, Vector3.zero, 0.2f)
                .setEaseOutQuad()
                .OnCompleteAsObservable()
                .DoOnCompleted(() => gameObject.SetActive(false))
                .Subscribe()
                .AddTo(disposer);
        }
    }
}
