using System;

namespace Modules.MiniGame.Scripts.Presentation
{
    public interface MiniGameWidgetView
    {
        event Action OnPlayButtonClicked;
        void InitGameView();
    }
}