using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Modules.Common
{
    public static class AssetCreator
    {
        public static void LoadAssetsAsync(IList<IResourceLocation> loadedLocations) {
            foreach (var location in loadedLocations)
            {
                Addressables.LoadAssetAsync<Object>(location);
            }
        }
    } 
}