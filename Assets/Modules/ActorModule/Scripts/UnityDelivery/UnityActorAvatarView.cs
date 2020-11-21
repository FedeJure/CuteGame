using System;
using System.Collections.Generic;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Presentation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.ActorModule.Scripts.UnityDelivery
{
    public class UnityActorAvatarView : MonoBehaviour, ActorAvatarView
    {
        [SerializeField] private TextMeshProUGUI name;
        [SerializeField] private Image actualHumor;
        [SerializeField] private List<HumorSprites> humorSprites;
        public event Action OnViewEnabled = () => { };
        public event Action OnViewDisabled = () => { };

        private void Awake()
        {
            ActorModuleProvider.ProvidePresenterFor(this);
        }

        public void SetName(string actorName)
        {
            name.text = actorName;
        }

        public void SetHumor(Humor humor)
        {
            humorSprites.
                ForEach(humorSprite =>
                {
                    if (humor.Equals(humorSprite.humor))
                        actualHumor.sprite = humorSprite.sprite;
                });
        }

        private void OnEnable()
        {
            OnViewEnabled();
        }
    }

    [Serializable]
    public class HumorSprites
    {
        [SerializeField] public Humor humor;
        [SerializeField] public Sprite sprite;
    }
}