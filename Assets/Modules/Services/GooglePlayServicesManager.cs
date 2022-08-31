using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UniRx;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Modules.Services
{
    
    public class GooglePlayServicesManager : MonoBehaviour
    {
        private void Start()
        {
            InitializePlayGamesLogin();
        }
        public static IObservable<bool> Login()
        {
            // #if UNITY_EDITOR
            // return Observable.Return(true);       
            // #endif
            
            var subject = new Subject<bool>();
            
            try
            {
                PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, async (result) =>
                {
                    Debug.Log("TOKEN: "+((PlayGamesLocalUser)Social.localUser).GetIdToken() + "ID: " + Social.localUser.id);
                    await AuthenticationService.Instance.SignInWithGoogleAsync(((PlayGamesLocalUser)Social.localUser).GetIdToken());
                    subject.OnNext(SignInStatus.Success.Equals(result));
                });
            }
            catch (Exception e)
            {
                subject.OnNext(false);
            }
            
            
            return subject;
        }

        public static ILocalUser GetLocalUser()
        {
            return Social.localUser;
        }
        
        async void InitializePlayGamesLogin()
        {
            var config = new PlayGamesClientConfiguration.Builder()
                .RequestServerAuthCode(false)
                .RequestIdToken()
                .Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            await UnityServices.InitializeAsync();
        }

    }
}
