using System;
using System.Collections.Generic;
using System.Linq;
using Modules.ActorModule.Scripts.Core.Domain.Repositories;
using Modules.ActorModule.Scripts.Presentation.Events;
using UnityEngine;

namespace Modules.ActorModule.Scripts.Core.Domain
{
    public class HumorStateService
    {
        private readonly HumorStateRepository humorRepository;
        private int MAX_HUMOR => 100;
        private int currentHumor;
        private List<List<HumorTransitionConfig>> humorTransitionConfig;
        public HumorStateService() { }
        public HumorStateService(HumorStateRepository humorRepository)
        {
            this.humorRepository = humorRepository;

            humorRepository.Get()
                .Do(state => { currentHumor = state.humorLevel; })
                .DoWhenAbsent(() =>
                {
                    var state = new HumorState(MAX_HUMOR / 2, 0, Humor.Normal, MAX_HUMOR);
                    currentHumor = state.humorLevel;
                    humorRepository.Save(state);
                });
            
            humorTransitionConfig = HumorStateGenerator.GetRandomConfigurationOfLength(MAX_HUMOR);
        }
        public virtual HumorState ReceiveInteraction(ActorInteraction interaction)
        {
            return humorRepository.Get()
                .ReturnOrDefault(state =>
                {
                    currentHumor = state.humorLevel;
                    var nextTransitions = GetNextTransition(interaction);
                    var humor = GetNextHumor(state.humorLevel, nextTransitions.humorReaction);
                    return new HumorState(Math.Max(state.humorLevel + nextTransitions.humorReaction, 0),
                        nextTransitions.humorReaction, humor, MAX_HUMOR);
                },new HumorState(0,0, Humor.Normal, MAX_HUMOR));

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
            var nextTransitions = humorTransitionConfig[currentHumor];
            nextTransitions.Add(new HumorTransitionConfig(interaction, 0));
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
        public int humorLevel;
        public int lastHumorChange;
        public Humor humor;
        public int maxHumor;

        public HumorState(int humorLevel, int lastHumorChange, Humor humor, int maxHumor)
        {
            this.humorLevel = humorLevel;
            this.lastHumorChange = lastHumorChange;
            this.humor = humor;
            this.maxHumor = maxHumor;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }

    public enum Humor
    {
        Happy,
        Normal,
        NotHappy
    }
}