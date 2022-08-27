using UnityEngine;

namespace Modules.MainGame.Scripts.Core.Domain
{
    public struct Session
    {
        public string playerId;
        public string actorId;

        public Session(string playerId, string actorId)
        {
            this.playerId = playerId;
            this.actorId = actorId;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}