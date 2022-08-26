using System;
using UniRx;
using UnityEngine;

namespace Modules.MainGame
{
    
    public class GooglePlayServicesManager : MonoBehaviour
    {
        private void Start()
        {
            Init();
        }

        static void Init()
        {
            // PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            // PlayGamesPlatform.InitializeInstance(config);
        }

        public static IObservable<bool> Login()
        {
            
            return Observable.Return(false);
            // var subject = new Subject<bool>();
            // PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (status) =>
            // {
            //     Debug.Log(status);
            //     subject.OnNext(SignInStatus.Success.Equals(status));
            // });
            // return subject;
        }

    }
}
