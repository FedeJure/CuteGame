using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Modules.Common
{
    public static class AddressableLocationLoader
    {
        public static async Task GetAll<T>(string label, IList<T> objects)
        where T : Object {
            IList<IResourceLocation> loadedLocation = new List<IResourceLocation>();
            var unloadedLocations = await Addressables.LoadResourceLocationsAsync(label).Task;
            foreach (var location in unloadedLocations)
            {
                loadedLocation.Add(location);
            }

            await AssetCreator.LoadAssetsAsync(loadedLocation, objects);
        }
    }
}