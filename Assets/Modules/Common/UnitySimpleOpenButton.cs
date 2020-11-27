using System;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Common
{
    public class UnitySimpleOpenButton : MonoBehaviour
    {
        [SerializeField] private OpenAnimation animationType;
        [SerializeField] private Button button;
        [SerializeField] private UnitySimpleClosableView view;
        [SerializeField] private RectTransform content;
        

        private Action action = () => { };

        private void Awake()
        {
            button.onClick.AddListener(OpenAction);
        }

        private void OpenAction()
        {
            switch (animationType)
            {
                case OpenAnimation.NO_ANIMATION:
                    view.gameObject.SetActive(true);
                    break;
                case OpenAnimation.FROM_BEHIND:
                {
                    view.gameObject.SetActive(true);
                    var scale = content.localScale;
                    content.localScale = Vector3.zero;
                    LeanTween.scale(content, scale,0.1f)
                        .setEaseInQuad();
                    break;
                }
            }
        }
    }

    internal enum OpenAnimation
    {
        NO_ANIMATION,
        FROM_BEHIND
    }
}