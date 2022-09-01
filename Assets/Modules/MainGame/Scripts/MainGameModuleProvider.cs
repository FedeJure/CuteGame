using Modules.ActorModule.Scripts;
using Modules.Common;
using Modules.MainGame.Scripts.Core.Actions;
using Modules.MainGame.Scripts.Infrastructure;
using Modules.MainGame.Scripts.Presentation;
using Modules.PlayerModule;

namespace Modules.MainGame.Scripts
{
    public static class MainGameModuleProvider
    {
        public static void ProvidePresenterFor(MainGameView view)
        {
            new MainGamePresenter(view,
                PlayerModuleProvider.ProvidePlayerRepository(),
                ProvideRequestLoginAction(),
                ActorModuleProvider.ProvideCreateActorAction(),
                CommonModuleProvider.ProvideGlobalEventBus(),
                ActorModuleProvider.ProvideRetrieveActorAction());
        }

        public static RequestLogin ProvideRequestLoginAction()
        {
            return DependencyProvider.GetOrInstanciate<RequestLogin>(() =>
                new RequestLogin(ProvideMainGameGateway(), PlayerModuleProvider.ProvidePlayerRepository(), CommonModuleProvider.ProvideSessionRepository(), ActorModuleProvider.ProvideRetrieveActorAction()));
        }

        public static MainGameGateway ProvideMainGameGateway()
        {
            return DependencyProvider.GetOrInstanciate<MainGameGateway>(() =>
                new GPSMainGameGateway());
        }
    }
}