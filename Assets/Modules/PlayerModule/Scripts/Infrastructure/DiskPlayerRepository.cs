using Modules.Common;
using Modules.PlayerModule.Scripts.Core.Domain;
using Modules.PlayerModule.Scripts.Core.Domain.Repositories;
using UnityEngine;

namespace Modules.PlayerModule.Scripts.Infrastructure
{
    public class DiskPlayerRepository : PlayerRepository
    {
        private const string key = "current_player";
        public Maybe<Player> Get()
        {
            return LocalStorage.Get(key)
                .ReturnOrDefault(value => JsonUtility.FromJson<Player>(value).ToMaybe(),
                    Maybe<Player>.Nothing);
        }

        public void Save(Player player)
        {
            LocalStorage.Save(key, player.ToString());
        }
    }
}