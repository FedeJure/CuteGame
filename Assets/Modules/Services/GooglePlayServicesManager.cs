using System;
using GooglePlayGames;
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
                Social.Active.Authenticate(Social.localUser,async (success, authCode) =>
                {
                    await UnityServices.InitializeAsync();

                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                    // await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(authCode);
                        
                    subject.OnNext(success);
                    // await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync("B7:AC:7A:81:84:7F:1F:95:9C:B8:3A:EE:0C:4E:DA:22:92:55:0F:84");
                });

                // PlayGamesPlatform.Instance.ManuallyAuthenticate((status) =>
                // {
                //     subject.OnNext(SignInStatus.Success.Equals(status));
                // });
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
        
        void InitializePlayGamesLogin()
        {
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
        }

    }
}
