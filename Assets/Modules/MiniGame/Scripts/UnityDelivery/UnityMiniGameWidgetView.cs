using System;
using System.Collections.Generic;
using System.Reflection;
using Modules.Common;
using Modules.MiniGame.Scripts.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.MiniGame.Scripts.UnityDelivery
{
    public class UnityMiniGameWidgetView: MonoBehaviour, MiniGameWidgetView
    {
        public event Action OnPlayButtonClicked = () => { };
        [SerializeField] private string addressableAssetLabel;
        [SerializeField] Button playButton;

        private void Awake()
        {
            MiniGameModuleProvider.ProvidePresenterFor(this);
            
            playButton.onClick.AddListener(OnPlayButtonClicked.Invoke);
        }

        public void InitGameView()
        {
            playButton.enabled = false;
            List<GameObject> gameObjects = new List<GameObject>();
            AddressableLocationLoader.GetAll(addressableAssetLabel, gameObjects);
        }
    }
}