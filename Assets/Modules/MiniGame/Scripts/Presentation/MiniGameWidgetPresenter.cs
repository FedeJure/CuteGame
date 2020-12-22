using Modules.Common;

namespace Modules.MiniGame.Scripts.Presentation
{
    public class MiniGameWidgetPresenter
    {
        private readonly MiniGameWidgetView widgetView;
        private readonly GlobalEventBus eventBus;

        public MiniGameWidgetPresenter(MiniGameWidgetView widgetView, GlobalEventBus eventBus)
        {
            this.widgetView = widgetView;
            this.eventBus = eventBus;

            widgetView.OnPlayButtonClicked += StartGame;
        }

        void StartGame()
        {
            eventBus.EmitOnMiniGameStarted();
            widgetView.InitGameView();
            widgetView.Close();
        }
    }
}