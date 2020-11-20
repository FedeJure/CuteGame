using Modules.MainGame.Scripts.Core.Domain;
using UnityEngine;

namespace Modules.Common
{
    public class SessionRepository
    {
        private const string key = "current_session";
        public virtual Maybe<Session> Get()
        {
            return LocalStorage.Get(key)
                .ReturnOrDefault(session => JsonUtility.FromJson<Session>(session).ToMaybe(),
                Maybe<Session>.Nothing);
        }

        public void Save(Session session)
        {
            LocalStorage.Save(key, session.ToString());
        }
        
    }
}