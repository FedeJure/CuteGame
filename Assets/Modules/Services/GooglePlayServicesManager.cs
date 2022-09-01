using System;
using System.Threading.Tasks;
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
        public static IObservable<bool> Login()
        {
            // #if UNITY_EDITOR
            // return Observable.Return(true);       
            // #endif
            var subject = new Subject<bool>();
            
            try
            {
                if (PlayGamesPlatform.Instance.IsAuthenticated())
                {
                    return  Observable.Return(true);
                }
                PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, async (result) =>
                {
                    try
                    {
                        Debug.Log("TOKEN: "+((PlayGamesLocalUser)Social.localUser).GetIdToken() + "ID: " + Social.localUser.id);
                        await AuthenticationService.Instance.SignInWithGoogleAsync(((PlayGamesLocalUser)Social.localUser).GetIdToken());
                        subject.OnNext(SignInStatus.Success.Equals(result));
                    }
                    catch (Exception e)
                    {
                        subject.OnNext(false);
                    }
                    
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
        
        public static async Task InitializePlayGamesLogin()
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

        public static bool ExistSession()
        {
            return AuthenticationService.Instance.SessionTokenExists;
        }

        public static void LogOut()
        {
            AuthenticationService.Instance.SignOut();
            PlayGamesPlatform.Instance.SignOut();
        }

    }
}
