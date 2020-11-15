using Modules.Common;

namespace Modules.Actor.Scripts.Core.Domain.Repositories
{
    public interface PlayerRepository
    {
        Maybe<Player> Get();
        void Save(Player player);
    }
}