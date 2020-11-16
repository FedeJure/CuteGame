
using Modules.PlayerModule.Scripts.Core.Domain;
using UnityEngine;

namespace Modules.ActorModule.Scripts.Core.Domain
{
    public class Actor
    {
        public string name;
        public long id;
        public ActorSkin skin;
        public Player owner;

        public Actor(long id, string name, ActorSkin skin, Player owner)
        {
            this.name = name;
            this.id = id;
            this.skin = skin;
            this.owner = owner;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}