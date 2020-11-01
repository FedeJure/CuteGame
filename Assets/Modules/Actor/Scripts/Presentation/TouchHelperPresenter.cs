using Modules.Actor.Scripts.Core;
using Modules.Actor.Scripts.Core.Domain.Action;
using Modules.Actor.Scripts.Presentation.Events;

namespace Modules.Actor.Scripts.Presentation
{
    public class TouchHelperPresenter
    {
        readonly TouchHelperView view;
        readonly ProcessDirectionAction processDirectionAction;

        public TouchHelperPresenter(TouchHelperView view, ProcessDirectionAction processDirectionAction)
        {
            this.view = view;
            this.processDirectionAction = processDirectionAction;

            this.view.OnViewEnabled += Present;
        }

        void Present()
        {
            view.OnSwipeAction += OnSwipeAction;
        }

        void OnSwipeAction(TouchDirection direction)
        {
            processDirectionAction.Execute(direction);    
        }
    }
}