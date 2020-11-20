using DependencyProviderNamespace;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Action;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.ActorModule.Scripts.Infrastructure;
using Modules.ActorModule.Scripts.Presentation;
using Modules.ActorModule.Scripts.UnityDelivery;
using Modules.Common;
using Modules.PlayerModule;

namespace Modules.ActorModule.Scripts
{
    public static class ActorModuleProvider
    {
        public static void ProvidePresenterFor(ActorView view)
        {
            new ActorPresenter(view, ProvideEventBus(), CommonModuleProvider.ProvideGlobalEventBus(), ProvideRetrieveHumorAction(), ProvideRetrieveActorAction());
        }

        private static RetrieveActor ProvideRetrieveActorAction()
        {
            return DependencyProvider.GetOrInstanciate(() => new RetrieveActor(ProvideActorRepository(), CommonModuleProvider.ProvideSessionRepository()));
        }

        public static void ProvidePresenterFor(HumorBarView view)
        {
            new HumorBarPresenter(view, ProvideEventBus(), ProvideRetrieveHumorAction());
        }

        public static RetrieveActorHumor ProvideRetrieveHumorAction()
        {
            return DependencyProvider.GetOrInstanciate(() => new RetrieveActorHumor(ProvideHumorStateRepository(), CommonModuleProvider.ProvideSessionRepository()));
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
            return DependencyProvider.GetOrInstanciate(() => new ProcessInteraction(ProvideEventBus(), ProvideHumorStateRepository(), ProvideHumorStateService(), CommonModuleProvider.ProvideSessionRepository()));
        }

        private static HumorStateService ProvideHumorStateService()
        {
            return DependencyProvider.GetOrInstanciate(() => new HumorStateService(ProvideHumorStateRepository(), CommonModuleProvider.ProvideSessionRepository()));
        }

        private static HumorStateRepository ProvideHumorStateRepository()
        {
            return DependencyProvider.GetOrInstanciate(() => new DiskHumorStateRepository());
        }

        public static HitTargetRepository ProvideHitTargetRepository()
        {
            return DependencyProvider.GetOrInstanciate(() => new HitTargetRepository());
        }

        public static ActorRepository ProvideActorRepository()
        {
            return DependencyProvider.GetOrInstanciate(() => new DiskActorRepository());
        }

        public static CreateNewActor ProvideCreateActorAction()
        {
            return DependencyProvider.GetOrInstanciate(() =>
                new CreateNewActor(ProvideActorRepository(), PlayerModuleProvider.ProvidePlayerRepository(), CommonModuleProvider.ProvideSessionRepository()));
        }
    }
}