using Modules.ActorModule.Scripts;
using Modules.Common;
using Modules.MiniGame.Scripts.Core.Domain;
using Modules.MiniGame.Scripts.Presentation;

namespace Modules.MiniGame
{
    public static class MiniGameModuleProvider
    {
        public static void ProvidePresenterFor(MiniGameWidgetView view)
        {
            new MiniGameWidgetPresenter(view, CommonModuleProvider.ProvideGlobalEventBus());
        }

        public static void ProvidePresenterFor(MiniGameUiView view)
        {
            new MiniGameUiPresenter(view, ProvideEventBus(), ActorModuleProvider.ProvideUpdateHumorFromScore());
        }

        public static MiniGameEventBus ProvideEventBus()
        {
            return DependencyProvider.GetOrInstanciate(() => new MiniGameEventBus());
        }
    }
}