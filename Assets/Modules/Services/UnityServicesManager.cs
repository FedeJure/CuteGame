using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Modules.Common;
using UniRx;
using Unity.Services.CloudSave;
using UnityEngine;

namespace Modules.Services
{
    public class UnityServicesManager: MonoBehaviour
    {
        private static Dictionary<string, string> userData = new Dictionary<string, string>();

        public static async Task Init()
        {
            userData = await CloudSaveService.Instance.Data.LoadAllAsync();
        }

        public static async Task LoadData()
        {
            userData = await CloudSaveService.Instance.Data.LoadAllAsync();
        }

        public static IObservable<Unit> Save(string key, object value)
        {
            var data = new Dictionary<string, object>{ { key, value } };
            return CloudSaveService.Instance.Data.ForceSaveAsync(data)
                .ToObservable()
                .SelectMany(_ =>
                    CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> {key})
                        .ToObservable())
                .Select(newData =>
                {
                    userData[key] = newData[key];
                    return Unit.Default;
                }).AsUnitObservable();
        }
        
        public static IObservable<Maybe<string>> Get(string key)
        {
            return Observable.Return(userData.ContainsKey(key) ? new Maybe<string>(userData[key]) : Maybe<string>.Nothing);
        }
    }
}