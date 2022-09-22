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
        private readonly HumorStateService humorStateService;
        private readonly SessionRepository sessionRepository;
        private readonly UpdateHumor updateHumor;
        readonly Dictionary<ActorInteraction, SimpleAction> eventMapper = new Dictionary<ActorInteraction, SimpleAction>();

        public ProcessInteraction() { }
        public ProcessInteraction(EventBus eventBus,
                                HumorStateService humorStateService,
                                SessionRepository sessionRepository,
                                UpdateHumor updateHumor)
        {
            this.humorStateService = humorStateService;
            this.sessionRepository = sessionRepository;
            this.updateHumor = updateHumor;

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
                    updateHumor.Execute(nextHumor, session.actorId);      
                });
        }
    }
}