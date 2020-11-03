using Modules.Actor.Scripts.Presentation.Events;
using UnityEngine;

namespace Modules.Actor.Scripts.Core.Domain.Action
{
    public class ProcessDirectionAction
    {
        readonly EventBus eventBus;

        public ProcessDirectionAction() { }
        public ProcessDirectionAction(EventBus eventBus)
        {
            this.eventBus = eventBus;
        }
        
        public virtual void Execute(ActorInteraction interaction)
        {
            Debug.LogWarning(interaction);
        }
    }
}