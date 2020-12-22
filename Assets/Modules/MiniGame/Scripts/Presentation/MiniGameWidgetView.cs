using System;
using System.Threading.Tasks;

namespace Modules.MiniGame.Scripts.Presentation
{
    public interface MiniGameWidgetView
    {
        event Action OnPlayButtonClicked;
        Task InitGameView();
        void Close();
    }
}