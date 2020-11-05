using System.Collections.Generic;
using Modules.Actor.Scripts.Core.Domain.Events;
using Modules.Actor.Scripts.Presentation.Events;

namespace Modules.Actor.Scripts.Core.Domain.Action
{

    internal delegate void SimpleAction();
    public class ProcessInteraction
    {
        readonly EventBus eventBus;
        readonly Dictionary<ActorInteraction, SimpleAction> eventMapper = new Dictionary<ActorInteraction, SimpleAction>();

        public ProcessInteraction() { }
        public ProcessInteraction(EventBus eventBus)
        {
            this.eventBus = eventBus;

            eventMapper[ActorInteraction.LeftCaress] = eventBus.EmitEvent<LeftCaressInteractionEvent>;
            eventMapper[ActorInteraction.RigthCaress] = eventBus.EmitEvent<RigthCaressInteractionEvent>;
            
        }
        
        public virtual void Execute(ActorInteraction interaction)
        {
            if (!eventMapper.ContainsKey(interaction)) return;
            eventMapper[interaction].Invoke();
        }
    }
}