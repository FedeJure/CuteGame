namespace Modules.Shop.Scripts.Presentation
{
    public class ShopPresenter
    {
        private ShopView view;

        public ShopPresenter(ShopView view)
        {
            this.view = view;

            this.view.OnViewEnable += PresentView;
            this.view.OnCloseClicked += CloseView;
        }

        private void CloseView()
        {
            view.Close();
        }

        private void PresentView()
        {
            view.ShowOpenAnimation();
        }
    }
}