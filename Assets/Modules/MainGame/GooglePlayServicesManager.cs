using System;
using GooglePlayGames;
using UniRx;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;

namespace Modules.MainGame
{
    
    public class GooglePlayServicesManager : MonoBehaviour
    {
        private void Start()
        {
            InitializePlayGamesLogin();
        }
        public static IObservable<bool> Login()
        {
            #if UNITY_EDITOR
            return Observable.Return(true);       
            #endif
            
            var subject = new Subject<bool>();
            Social.localUser.Authenticate((success, authCode) =>
            {
                subject.OnNext(success);
            });
            return subject;
        }

        public static ILocalUser GetLocalUser()
        {
            return Social.localUser;
        }
        
        void InitializePlayGamesLogin()
        {
            PlayGamesPlatform.Activate();
        }

    }
}
