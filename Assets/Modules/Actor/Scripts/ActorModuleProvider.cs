using Modules.Actor.Scripts.Core;
using Modules.Actor.Scripts.Presentation;
using DependencyProviderNamespace;
using Modules.Actor.Scripts.Core.Domain;
using Modules.Actor.Scripts.Core.Domain.Action;
using Modules.Actor.Scripts.Infrastructure;
using Modules.Actor.Scripts.UnityDelivery;

namespace Modules.Actor.Scripts
{
    public static class ActorModuleProvider
    {
        public static void ProvidePresenterFor(ActorView view)
        {
            new ActorPresenter(view, ProvideEventBus(), ProvideRetrieveHumorAction());
        }

        public static RetrieveActorHumor ProvideRetrieveHumorAction()
        {
            return DependencyProvider.GetOrInstanciate(() => new RetrieveActorHumor(ProvideHumorStateRepository()));
        }

        public static EventBus ProvideEventBus()
        {
            return DependencyProvider.GetOrInstanciate(() => new EventBus());
        }

        public static void ProvidePresenterFor(TouchHelperView view)
        {
            new TouchHelperPresenter(view, ProvideProcessDirectionAction());
        }

        private static ProcessInteraction ProvideProcessDirectionAction()
        {
            return DependencyProvider.GetOrInstanciate(() => new ProcessInteraction(ProvideEventBus(), ProvideHumorStateRepository(), ProvideHumorStateService()));
        }

        private static HumorStateService ProvideHumorStateService()
        {
            return DependencyProvider.GetOrInstanciate(() => new HumorStateService(ProvideHumorStateRepository()));
        }

        private static HumorStateRepository ProvideHumorStateRepository()
        {
            return DependencyProvider.GetOrInstanciate(() => new InMemoryHumorStateRepository());
        }

        public static HitTargetRepository ProvideHitTargetRepository()
        {
            return DependencyProvider.GetOrInstanciate(() => new HitTargetRepository());
        }
    }
}