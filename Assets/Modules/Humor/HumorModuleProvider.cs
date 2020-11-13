using Modules.Actor.Scripts;
using Modules.Humor.Presentation;

namespace Modules.Humor
{
    public static class HumorModuleProvider
    {
        public static void ProvidePresenterFor(HumorBarView view)
        {
            new HumorBarPresenter(view, ActorModuleProvider.ProvideEventBus(), ActorModuleProvider.ProvideRetrieveHumorAction());
        }
    }
}