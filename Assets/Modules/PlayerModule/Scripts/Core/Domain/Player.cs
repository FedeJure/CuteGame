using System;
using UnityEngine;

namespace Modules.PlayerModule.Scripts.Core.Domain
{
    [Serializable]
    public class Player
    {
        public string id;
        
        public Player() { }

        public Player(string id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}