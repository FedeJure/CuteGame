using System;
using System.Collections.Generic;
using Modules.Actor.Scripts.Core.Domain.Events;
using Modules.Actor.Scripts.Infrastructure;
using Modules.Actor.Scripts.Presentation.Events;

namespace Modules.Actor.Scripts.Core.Domain.Action
{

    internal delegate void SimpleAction();
    public class ProcessInteraction
    {
        readonly EventBus eventBus;
        private readonly HumorStateRepository humorStateRepository;
        private readonly HumorStateService humorStateService;
        readonly Dictionary<ActorInteraction, SimpleAction> eventMapper = new Dictionary<ActorInteraction, SimpleAction>();

        public ProcessInteraction() { }
        public ProcessInteraction(EventBus eventBus,
                                HumorStateRepository humorStateRepository,
                                HumorStateService humorStateService)
        {
            this.eventBus = eventBus;
            this.humorStateRepository = humorStateRepository;
            this.humorStateService = humorStateService;

            eventMapper[ActorInteraction.LeftCaress] = eventBus.EmitEvent<LeftCaressInteractionEvent>;
            eventMapper[ActorInteraction.RigthCaress] = eventBus.EmitEvent<RigthCaressInteractionEvent>;
            
        }
        
        public virtual void Execute(ActorInteraction interaction)
        {
            UpdateState(interaction);
            if (!eventMapper.ContainsKey(interaction)) return;
            eventMapper[interaction].Invoke();
        }

        private void UpdateState(ActorInteraction interaction)
        {
            var nextHumor = humorStateService.ReceiveInteraction(interaction);
            humorStateRepository.Save(nextHumor);
            
            eventBus.EmitEvent<HumorChangesEvent>();

            if (nextHumor.lastHumorChange > 0)
            {
                eventBus.EmitEvent<HappyEvent>();
            }
            else
            {
                eventBus.EmitEvent<NotHappyEvent>();
            }
            
        }
    }
}