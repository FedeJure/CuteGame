using System;

namespace Modules.MiniGame.Scripts.Presentation
{
    public interface MiniGameView
    {
        event Action OnViewEnabled;
        event Action OnViewDisabled;
    }
}