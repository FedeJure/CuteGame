using System.Collections.Generic;
using Modules.Actor.Scripts.Presentation.Events;
using UnityEngine;

namespace Modules.Actor.Scripts.Core.Domain
{
    public class HumorStateGenerator
    {
        private static readonly IReadOnlyList<ActorInteraction> possibleInteractions = new List<ActorInteraction>()
        {
            ActorInteraction.Consent,
            ActorInteraction.Joke,
            ActorInteraction.LeftCaress,
            ActorInteraction.LeftTickle,
            ActorInteraction.RigthCaress,
            ActorInteraction.RigthTickle
        };

        public static List<List<HumorTransitionConfig>> GetRandomConfigurationOfLength(int length)
        {
            var config = new List<List<HumorTransitionConfig>>();

            for (int i = 0; i < length / 3; i++)
            {
                config.Add(GetConfigWithGoods(2, 1, -1));
            }

            for (int i = length / 3; i < length * 2 / 3; i++)
            {
                config.Add(GetConfigWithGoods(1, 1, -1));
            }

            for (int i = length * 2 / 3; i < length; i++)
            {
                config.Add(GetConfigWithGoods(1, 1, -2));
            }
            
            return config;
        }

        private static List<HumorTransitionConfig> GetConfigWithGoods(int goodsCount, int goodPoints, int badPoints)
        {
            var config = new List<HumorTransitionConfig>();
            var aux = new List<ActorInteraction>(possibleInteractions);
            for (int i = 0; i < goodsCount; i++)
            {
                int index = Random.Range(0, aux.Count - 1);
                config.Add(new HumorTransitionConfig(aux[index], goodPoints));
                aux.RemoveAt(index);
            }
            aux.ForEach(interaction => config.Add(new HumorTransitionConfig(interaction, badPoints)));
            return config;
        }
    }
}