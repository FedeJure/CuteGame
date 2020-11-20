using UnityEngine;

namespace Modules.MainGame.Scripts.Core.Domain
{
    public struct Session
    {
        public long playerId;
        public long actorId;

        public Session(long playerId, long actorId)
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