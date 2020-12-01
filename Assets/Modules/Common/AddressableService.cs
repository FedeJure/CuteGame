using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Modules.Common
{
    public static class AddressableService
    {
        public static async Task DownloadAssetsOfLabel(string label) {
            var unloadedLocations = await Addressables.LoadResourceLocationsAsync(label).Task;
            foreach (var location in unloadedLocations)
            {
                Addressables.LoadAssetAsync<Object>(location);
            }
        }

        public static async Task InstantiateAsyncFrom(AssetReference location)
        {
            Addressables.InstantiateAsync(location);
        }
    }
}