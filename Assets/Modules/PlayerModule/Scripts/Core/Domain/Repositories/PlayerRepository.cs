
using Modules.Common;

namespace Modules.PlayerModule.Scripts.Core.Domain.Repositories
{
    public interface PlayerRepository
    {
        Maybe<Player> Get();
        void Save(Player player);
    }
}