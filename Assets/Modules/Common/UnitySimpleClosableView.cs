using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Common
{
    public class UnitySimpleClosableView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private CloseAnimation animationType;
        [SerializeField] private RectTransform content;
        
        private Action action = () => { };
        
        private void Awake()
        {
            button.onClick.AddListener(() => action());

            if (CloseAnimation.NO_ANIMATION.Equals(animationType))
            {
                action = () => { gameObject.SetActive(false);};
            }
            if (CloseAnimation.TO_BEHIND.Equals(animationType))
            {
                action = () =>
                {
                    var scale = content.localScale;
                    LeanTween.scale(content, Vector3.zero, 0.1f)
                        .setEaseInQuad()
                        .OnCompleteAsObservable()
                        .DoOnCompleted(() =>
                        {
                            gameObject.SetActive(false);
                            content.localScale = scale;
                        })
                        .Subscribe();
                };
            }
        }
    }

    public enum CloseAnimation
    {
        NO_ANIMATION,
        TO_BEHIND
    }
}