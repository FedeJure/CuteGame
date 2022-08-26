using System;
using GooglePlayGames;
using UniRx;
using UnityEngine;

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
        
        void InitializePlayGamesLogin()
        {
            PlayGamesPlatform.Activate();
        }

    }
}
