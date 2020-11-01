using Modules.Actor.Scripts.Core;

namespace Modules.Actor.Scripts.Presentation
{
    public class TouchHelperPresenter
    {
        readonly TouchHelperView view;
        readonly EventBus eventBus;

        public TouchHelperPresenter(TouchHelperView view, EventBus eventBus)
        {
            this.view = view;
            this.eventBus = eventBus;
        }
    }
}