using System.Collections.Generic;
using System.Threading.Tasks;
using Modules.Common;
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

        public static async Task<bool> LoadData()
        {
            userData = await CloudSaveService.Instance.Data.LoadAllAsync();
            return false;
        }

        public static async void Save(string key, object value)
        {
            var data = new Dictionary<string, object>{ { key, value } };
            await CloudSaveService.Instance.Data.ForceSaveAsync(data);
            var newData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> {key});
            Debug.Log(value);
            Debug.Log(newData[key]);
            userData[key] = newData[key];
        }
        
        public static Maybe<string> Get(string key)
        {
            return new Maybe<string>(userData.ContainsKey(key) ? userData[key] : null);
        }
    }
}