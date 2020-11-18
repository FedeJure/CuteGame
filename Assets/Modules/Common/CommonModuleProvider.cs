using DependencyProviderNamespace;

namespace Modules.Common
{
    public static class CommonModuleProvider
    {
        public static GlobalEventBus ProvideGlobalEventBus()
        {
            return DependencyProvider.GetOrInstanciate<GlobalEventBus>(() => new GlobalEventBus());
        }
    }
}