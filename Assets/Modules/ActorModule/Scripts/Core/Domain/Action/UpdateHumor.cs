using Modules.ActorModule.Scripts.Core.Domain.Events;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;

namespace Modules.ActorModule.Scripts.Core.Domain.Action
{
    public class UpdateHumor
    {
        private EventBus eventBus;
        private HumorStateRepository humorStateRepository;
        public UpdateHumor() {}
        public UpdateHumor(EventBus eventBus,
            HumorStateRepository humorStateRepository)
        {
            this.eventBus = eventBus;
            this.humorStateRepository = humorStateRepository;
        }
        public virtual void Execute(HumorState humor, string actorId)
        {
            humorStateRepository.Save(humor, actorId);
            
            if (humor.lastHumorChange == 0) return;
            eventBus.EmitEvent<HumorChangesEvent>();

            if (humor.lastHumorChange > 0)
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