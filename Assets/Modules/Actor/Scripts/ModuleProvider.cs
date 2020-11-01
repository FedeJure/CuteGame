using Modules.Actor.Scripts.Core;
using Modules.Actor.Scripts.Presentation;
using DependencyProviderNamespace;

namespace Modules.Actor.Scripts
{
    public class ModuleProvider
    {
        public static void ProvidePresenterFor(ActorView view)
        {
            new ActorPresenter(view, ProvideEventBus());
        }

        private static EventBus ProvideEventBus()
        {
            return DependencyProvider.GetOrInstanciate(() => new EventBus());
        }

        public static void ProvidePresenterFor(TouchHelperView view)
        {
            new TouchHelperPresenter(view, ProvideEventBus());
        }
    }
}