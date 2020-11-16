using Modules.ActorModule.Scripts.Core.Domain.Action;
using Modules.ActorModule.Scripts.Presentation.Events;

namespace Modules.ActorModule.Scripts.Presentation
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