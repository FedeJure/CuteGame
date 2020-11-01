using System;
using System.Collections.Generic;

namespace DependencyProviderNamespace
{
    public static class DependencyProvider
    {
        private static Dictionary<Type, object> instances = new Dictionary<Type, object>();
        
        public static T GetOrInstanciate<T>(Func<T> createInstance)
        {
            if (!instances.ContainsKey(typeof(T))) instances.Add(typeof(T), createInstance());
            return (T)instances[typeof(T)];
        }
    }
}