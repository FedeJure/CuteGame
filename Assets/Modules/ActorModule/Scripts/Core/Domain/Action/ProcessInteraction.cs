using System.Collections.Generic;
using Modules.ActorModule.Scripts.Core.Domain.Events;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.ActorModule.Scripts.Presentation.Events;
using Modules.Common;
using Modules.PlayerModule.Scripts.Core.Domain.Repositories;
using UnityEngine;

namespace Modules.ActorModule.Scripts.Core.Domain.Action
{

    internal delegate void SimpleAction();
    public class ProcessInteraction
    {
        readonly EventBus eventBus;
        private readonly HumorStateRepository humorStateRepository;
        private readonly HumorStateService humorStateService;
        private readonly SessionRepository sessionRepository;
        readonly Dictionary<ActorInteraction, SimpleAction> eventMapper = new Dictionary<ActorInteraction, SimpleAction>();

        public ProcessInteraction() { }
        public ProcessInteraction(EventBus eventBus,
                                HumorStateRepository humorStateRepository,
                                HumorStateService humorStateService,
                                SessionRepository sessionRepository)
        {
            this.eventBus = eventBus;
            this.humorStateRepository = humorStateRepository;
            this.humorStateService = humorStateService;
            this.sessionRepository = sessionRepository;

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
            sessionRepository.Get()
                .Do(session =>
                {
                    var nextHumor = humorStateService.ReceiveInteraction(interaction);
                    humorStateRepository.Save(nextHumor, session.actorId);
            
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
                });
        }
    }
}