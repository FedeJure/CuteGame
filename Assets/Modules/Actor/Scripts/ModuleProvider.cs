using Modules.Actor.Scripts.Core;
using Modules.Actor.Scripts.Presentation;
using DependencyProviderNamespace;
using Modules.Actor.Scripts.Core.Domain.Action;
using Modules.Actor.Scripts.Core.Domain.Services;
using Modules.Actor.Scripts.Infrastructure;
using Modules.Actor.Scripts.UnityDelivery;

namespace Modules.Actor.Scripts
{
    public static class ModuleProvider
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
            new TouchHelperPresenter(view, ProvideProcessDirectionAction());
        }

        private static ProcessInteraction ProvideProcessDirectionAction()
        {
            return DependencyProvider.GetOrInstanciate(() => new ProcessInteraction(ProvideEventBus(), ProvideActorStateRepository(), ProvideHumorStateService()));
        }

        private static HumorStateService ProvideHumorStateService()
        {
            return DependencyProvider.GetOrInstanciate(() => new HumorStateService());
        }

        private static ActorStateRepository ProvideActorStateRepository()
        {
            return DependencyProvider.GetOrInstanciate(() => new InMemoryStateRepository());
        }

        public static HitTargetRepository ProvideHitTargetRepository()
        {
            return DependencyProvider.GetOrInstanciate(() => new HitTargetRepository());
        }
    }
}