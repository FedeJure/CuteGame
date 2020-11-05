using Modules.Actor.Scripts.Core.Domain.Action;
using Modules.Actor.Scripts.Presentation.Events;

namespace Modules.Actor.Scripts.Presentation
{
    public class TouchHelperPresenter
    {
        readonly TouchHelperView view;
        readonly ProcessInteraction _processInteraction;

        public TouchHelperPresenter(TouchHelperView view, ProcessInteraction processInteraction)
        {
            this.view = view;
            this._processInteraction = processInteraction;

            this.view.OnViewEnabled += Present;
        }

        void Present()
        {
            view.OnActorInteraction += interaction => OnActorInteraction(interaction);
        }

        void OnActorInteraction(ActorInteraction interaction)
        {
            _processInteraction.Execute(interaction);    
        }
    }
}