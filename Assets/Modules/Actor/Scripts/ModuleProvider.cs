using Modules.Actor.Scripts.Core;
using Modules.Actor.Scripts.Presentation;
using DependencyProviderNamespace;

namespace Modules.Actor.Scripts
{
    public class ModuleProvider
    {
        public static ActorPresenter ProvidePresenterFor(ActorView view)
        {
            return new ActorPresenter(view, ProvideEventBus());
        }

        public static EventBus ProvideEventBus()
        {
            return DependencyProvider.GetOrInstanciate(() => new EventBus());
        }
    }
}