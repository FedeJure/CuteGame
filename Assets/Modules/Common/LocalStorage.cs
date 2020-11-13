using UnityEngine;

namespace Modules.Common
{
    public static class LocalStorage
    {
        public static void Save(string key, string value)
        {
            PlayerPrefs.SetString(GetKey(key), value);
            PlayerPrefs.Save();
        }

        public static Maybe<string> Get(string key)
        {
            var indexKey = GetKey(key);
            return PlayerPrefs.HasKey(indexKey) ? new Maybe<string>(PlayerPrefs.GetString(indexKey)) : Maybe<string>.Nothing;
        }

        private static string GetKey(string key)
        {
            //TODO: Agregar user id en un futuro
            return key;
        }
    }
}