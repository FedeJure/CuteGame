using System;
using Modules.Common;
using Modules.MiniGame.Scripts.Presentation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Task = System.Threading.Tasks.Task;

namespace Modules.MiniGame.Scripts.UnityDelivery
{
    public class UnityMiniGameWidgetView: MonoBehaviour, MiniGameWidgetView
    {
        public event Action OnPlayButtonClicked = () => { };
        [SerializeField] private string addressableAssetLabel;
        [SerializeField] Button playButton;
        [SerializeField] private AssetReference mainPrefabLocation;
        [SerializeField] private Button closeButton;

        private void Awake()
        {
            MiniGameModuleProvider.ProvidePresenterFor(this);
            
            playButton.onClick.AddListener(OnPlayButtonClicked.Invoke);
        }

        public void OnEnable() {
            playButton.enabled = true;
        }

        public async Task InitGameView()
        {
            playButton.enabled = false;
            await AddressableService.DownloadAssetsOfLabel(addressableAssetLabel);
            await AddressableService.InstantiateAsyncFrom(mainPrefabLocation);
        }

        public void Close()
        {
            closeButton.onClick.Invoke();
        }
    }
}