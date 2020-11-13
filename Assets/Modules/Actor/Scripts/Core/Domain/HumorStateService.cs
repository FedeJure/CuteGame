using System;
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
        private int MAX_HUMOR => 100;
        private int currentHumor;
        private List<List<HumorTransitionConfig>> humorTransitionConfig;
        private int currentHumorIndex;
        public HumorStateService() { }
        public HumorStateService(HumorStateRepository humorRepository)
        {
            this.humorRepository = humorRepository;
            currentHumor = 50;
            
            humorRepository.Save(new HumorState(currentHumor, 0, Humor.Normal, MAX_HUMOR));
            
            //Todo configuracion diferente en cada nacimiento.
            humorTransitionConfig = HumorStateGenerator.GetRandomConfigurationOfLength(MAX_HUMOR);
        }
        public virtual HumorState ReceiveInteraction(ActorInteraction interaction)
        {
            var savedHumor = humorRepository.Get();
            var nextTransitions = GetNextTransition(interaction);
            var humor = GetNextHumor(savedHumor.humorLevel, nextTransitions.humorReaction);
            return new HumorState(savedHumor.humorLevel + nextTransitions.humorReaction, nextTransitions.humorReaction, humor, MAX_HUMOR);
        }

        private Humor GetNextHumor(int humorLevel, int humorVariation)
        {
            var newHumorLevel = humorLevel + humorVariation;
            if (newHumorLevel < (int) Math.Floor((double) (MAX_HUMOR / 3))) return Humor.NotHappy;
            if (newHumorLevel > (int) Math.Floor((double) (MAX_HUMOR / 3)) && newHumorLevel < (int) Math.Floor((double) (2 * MAX_HUMOR / 3))) return Humor.Normal;
            return Humor.Happy;
        }

        private HumorTransitionConfig GetNextTransition(ActorInteraction interaction)
        {
            var nextTransitions = humorTransitionConfig[currentHumorIndex];
            nextTransitions.Add(new HumorTransitionConfig(interaction, 0));
            currentHumorIndex = currentHumorIndex == humorTransitionConfig.Count - 1 ? 0 : currentHumorIndex + 1;
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
        public HumorState(int humorLevel, int lastHumorChange, Humor humor, int maxHumor)
        {
            this.humorLevel = humorLevel;
            this.lastHumorChange = lastHumorChange;
            this.humor = humor;
            this.maxHumor = maxHumor;
        }
        public int humorLevel { get; }
        public int lastHumorChange { get; }
        public Humor humor { get; }
        public int maxHumor { get; }
    }

    public enum Humor
    {
        Happy,
        Normal,
        NotHappy
    }
}