using System;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;

namespace Modules.Common
{
    public class Interstitial : MonoBehaviour
    {
        static IInterstitialAd ad;
        static string adUnitId = "Interstitial_Android";
        static string gameId = "4911293";

        private void Start()
        {
            InitServices();
        }

        static public void ShowInterstitial()
        {
            SetupAd();
            ShowAd();
        }

        private async void InitServices()
        {
            try
            {
                InitializationOptions initializationOptions = new InitializationOptions();
                initializationOptions.SetGameId(gameId);
                await UnityServices.InitializeAsync(initializationOptions);

                InitializationComplete();
            }
            catch (Exception e)
            {
                InitializationFailed(e);
            }
        }

        static private void SetupAd()
        {
            //Create
            ad = MediationService.Instance.CreateInterstitialAd(adUnitId);
        }
        
        static private async void ShowAd()
        {
            if (ad.AdState == AdState.Loaded)
            {
                try
                {
                    InterstitialAdShowOptions showOptions = new InterstitialAdShowOptions();
                    showOptions.AutoReload = true;
                    await ad.ShowAsync(showOptions);
                }
                catch (ShowFailedException e)
                {
                }
            }
        }

        void InitializationComplete()
        {
            SetupAd();
            LoadAd();
        }

        async void LoadAd()
        {
            try
            {
                await ad.LoadAsync();
            }
            catch (LoadFailedException)
            {
                // We will handle the failure in the OnFailedLoad callback
            }
        }

        void InitializationFailed(Exception e)
        {
            Debug.Log("Initialization Failed: " + e.Message);
        }

        void AdFailedShow(ShowFailedException e)
        {
            Debug.Log(e.Message);
        }

        void ImpressionEvent(object sender, ImpressionEventArgs args)
        {
            var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
            Debug.Log("Impression event from ad unit id " + args.AdUnitId + " " + impressionData);
        }
        
    }
}