using Modules.Common;
using Modules.PlayerModule.Scripts.Core.Domain.Repositories;
using Modules.PlayerModule.Scripts.Infrastructure;

namespace Modules.PlayerModule
{
    public static class PlayerModuleProvider
    {
        public static PlayerRepository ProvidePlayerRepository()
        {
            return DependencyProvider.GetOrInstanciate<PlayerRepository>(() =>
                new DiskPlayerRepository());
        }
    }
}