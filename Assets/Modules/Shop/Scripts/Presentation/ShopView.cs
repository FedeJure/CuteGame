using System;

namespace Modules.Shop.Scripts.Presentation
{
    public interface ShopView
    {
        event Action OnViewEnable;
        event Action OnCloseClicked;
        void ShowOpenAnimation();
        void Close();
    }
}