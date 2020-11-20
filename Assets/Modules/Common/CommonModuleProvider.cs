using DependencyProviderNamespace;

namespace Modules.Common
{
    public static class CommonModuleProvider
    {
        public static GlobalEventBus ProvideGlobalEventBus()
        {
            return DependencyProvider.GetOrInstanciate(() => new GlobalEventBus());
        }

        public static SessionRepository ProvideSessionRepository()
        {
            return DependencyProvider.GetOrInstanciate(() => new SessionRepository());
        }
    }
}