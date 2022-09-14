
using System;
using Modules.PlayerModule.Scripts.Core.Domain;
using UnityEngine;

namespace Modules.ActorModule.Scripts.Core.Domain
{
    [Serializable]
    public class Actor
    {
        public string name;
        public string id;
        public ActorSkin skin;
        public Player owner;
        
        public Actor() { }
        public Actor(string id, string name, ActorSkin skin, Player owner)
        {
            this.name = name;
            this.id = id;
            this.skin = skin;
            this.owner = owner;
        }

        public override string ToString()
        {
            Debug.LogWarning(JsonUtility.ToJson(this));
            return JsonUtility.ToJson(this);
        }
    }
}