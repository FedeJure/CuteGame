using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Common
{
    public class ConfirmationModal : MonoBehaviour
    {
        private static ConfirmationModal instance;
        [SerializeField] private GameObject content;
        [SerializeField] private Button okButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI text;
        

        private void Start()
        {
            instance = this;
        }

        public static IObservable<bool> Open(string title, string text)
        {
            return instance ? instance._Open(title,text) : Observable.Return(false);
        }

        private IObservable<bool> _Open(string title, string text)
        {
            if (content.activeSelf) return Observable.Return(false);
            this.title.text = title;
            this.text.text = text;
            var response = new Subject<bool>();
            okButton.onClick.AddListener(() =>
            {
                response.OnNext(true);
                response.OnCompleted();
            });
            cancelButton.onClick.AddListener(() =>
            {
                response.OnNext(false);
                response.OnCompleted();
            });
            content.SetActive(true);
            return response
                .DoOnCompleted(() =>
                {
                    okButton.onClick.RemoveAllListeners();
                    cancelButton.onClick.RemoveAllListeners();
                });
        }
    }
}
