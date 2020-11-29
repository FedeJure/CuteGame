using Modules.Common;

namespace Modules.MiniGame.Scripts.Presentation
{
    public class MiniGamePresenter
    {
        private readonly GlobalEventBus globalEventBus;

        public MiniGamePresenter(MiniGameView view, GlobalEventBus globalEventBus)
        {
            this.globalEventBus = globalEventBus;

            view.OnViewEnabled += _PresentView;
            view.OnViewDisabled += _DisposeView;
        }

        private void _PresentView()
        {
            globalEventBus.EmitOnMiniGameStarted();
            PresentView();
        }

        private void _DisposeView()
        {
            globalEventBus.EmitOnMiniGameEnded();
            DisposeView();
        }

        protected virtual void PresentView() { }
        protected virtual void DisposeView() { }
    }
}