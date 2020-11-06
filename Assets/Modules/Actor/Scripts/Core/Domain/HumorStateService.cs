using System.Collections.Generic;
using System.Linq;
using Modules.Actor.Scripts.Infrastructure;
using Modules.Actor.Scripts.Presentation.Events;
using UniRx.InternalUtil;

namespace Modules.Actor.Scripts.Core.Domain
{
    public class HumorStateService
    {
        private readonly HumorStateRepository humorRepository;
        const int maxHumor = 100;
        private int currentHumor;
        private ImmutableList<List<HumorTransitionConfig>> humorTransitionConfig;
        private int currentHumorIndex = 0;
        public HumorStateService(HumorStateRepository humorRepository)
        {
            this.humorRepository = humorRepository;
            currentHumor = 50;
            
            humorRepository.Save(new HumorState(currentHumor, 0));
            
            //Todo configuracion diferente en cada nacimiento.
            humorTransitionConfig = new ImmutableList<List<HumorTransitionConfig>>(new []
            {
                new List<HumorTransitionConfig>(new []
                {
                    new HumorTransitionConfig(ActorInteraction.LeftCaress, 1),
                    new HumorTransitionConfig(ActorInteraction.RigthCaress, -1),
                }),
                new List<HumorTransitionConfig>(new []
                {
                    new HumorTransitionConfig(ActorInteraction.LeftCaress, -1),
                    new HumorTransitionConfig(ActorInteraction.RigthCaress, 1),
                }),
                new List<HumorTransitionConfig>(new []
                {
                    new HumorTransitionConfig(ActorInteraction.LeftCaress, 1),
                    new HumorTransitionConfig(ActorInteraction.RigthCaress, -1),
                }),
                new List<HumorTransitionConfig>(new []
                {
                    new HumorTransitionConfig(ActorInteraction.LeftCaress, -1),
                    new HumorTransitionConfig(ActorInteraction.RigthCaress, 1),
                }),
                new List<HumorTransitionConfig>(new []
                {
                    new HumorTransitionConfig(ActorInteraction.LeftCaress, 1),
                    new HumorTransitionConfig(ActorInteraction.RigthCaress, -1),
                }),

            });
        }
        public HumorState ReceiveInteraction(ActorInteraction interaction)
        {
            var savedHumor = humorRepository.Get();
            var nextTransitions = GetNextTransition(interaction);
            return new HumorState(savedHumor.humor + nextTransitions.humorReaction, nextTransitions.humorReaction);
        }

        private HumorTransitionConfig GetNextTransition(ActorInteraction interaction)
        {
            var nextTransitions = humorTransitionConfig.Data[currentHumorIndex];
            nextTransitions.Add(new HumorTransitionConfig(interaction, 0));
            currentHumorIndex = currentHumorIndex == humorTransitionConfig.Data.Length - 1 ? 0 : currentHumorIndex + 1;
            return nextTransitions.First(transition => transition.interaction.Equals(interaction));  
        }
    }

    public class HumorTransitionConfig
    {
        public HumorTransitionConfig(ActorInteraction actorInteraction, int humorReaction)
        {
            interaction = actorInteraction;
            this.humorReaction = humorReaction;
        }
        public ActorInteraction interaction { get; }
        public int humorReaction { get; }
    }

    public class HumorState
    {
        public HumorState(int humor, int lastHumorChange)
        {
            this.humor = humor;
            this.lastHumorChange = lastHumorChange;
        }
        public int humor { get; }
        public int lastHumorChange { get; }
    }
}