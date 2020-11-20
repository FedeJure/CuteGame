using System;
using UnityEngine;

namespace Modules.PlayerModule.Scripts.Core.Domain
{
    [Serializable]
    public class Player
    {
        public long id;
        
        public Player() { }

        public Player(long id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}