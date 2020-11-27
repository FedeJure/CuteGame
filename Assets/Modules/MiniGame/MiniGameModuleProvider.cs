using Modules.Common;
using Modules.MiniGame.Scripts.Presentation;

namespace Modules.MiniGame
{
    public static class MiniGameModuleProvider
    {
        public static void ProvidePresenterFor(MiniGameWidgetView view)
        {
            new MiniGameWidgetPresenter(view, CommonModuleProvider.ProvideGlobalEventBus());
        }
    }
}