using Modules.Shop.Scripts.Presentation;

namespace Modules.Shop
{
    public static class ShopModuleProvider
    {
        public static void ProvidePresenterFor(ShopView view)
        {
            new ShopPresenter(view);
        }
    }
}