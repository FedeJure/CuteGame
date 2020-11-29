using Modules.Common;
using Modules.UlaGame.Scripts.Core.Actions;
using Modules.UlaGame.Scripts.Core.Domain;
using Modules.UlaGame.Scripts.Presentation;

namespace Modules.UlaGame
{
    public static class UlaGameModuleProvider
    {
        public static void ProvidePresenterFor(UlaGameView view)
        {
            new UlaGamePresenter(view,
                CommonModuleProvider.ProvideGlobalEventBus(),
                ProvideStartUlaGameAction(),
                ProvideUlaGameEventBus());
        }

        private static StartUlaGameAction ProvideStartUlaGameAction()
        {
            return DependencyProvider.GetOrInstanciate(() => 
                new StartUlaGameAction(ProvideUlaGameEventBus()));
        }

        public static UlaGameEventBus ProvideUlaGameEventBus()
        {
            return DependencyProvider.GetOrInstanciate(() => new UlaGameEventBus());
        }
    }
}