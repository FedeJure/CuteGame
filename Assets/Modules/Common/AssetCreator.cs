using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Modules.Common
{
    public static class AssetCreator
    {
        public static async Task LoadAssetsAsync<T>(IList<IResourceLocation> loadedLocations, IList<T> createdObjects)
        where T : Object {
            foreach (var location in loadedLocations)
            {
                var obj = await Addressables.InstantiateAsync(location).Task as T;
                createdObjects.Add(obj);
            }
        }
    } 
}