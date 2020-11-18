﻿using System.Collections.Generic;
using Modules.ActorModule.Scripts.Core.Domain.Events;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.ActorModule.Scripts.Presentation.Events;

namespace Modules.ActorModule.Scripts.Core.Domain.Action
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
            eventMapper[ActorInteraction.LeftTickle] = eventBus.EmitEvent<LeftTickleInteractionEvent>;
            eventMapper[ActorInteraction.RigthTickle] = eventBus.EmitEvent<RightTickleInteractionEvent>;
            eventMapper[ActorInteraction.Consent] = eventBus.EmitEvent<MiddleConsentEvent>;
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
            
            if (nextHumor.lastHumorChange == 0) return;
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