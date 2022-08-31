using System;
using Modules.Common;
using UniRx;

namespace Modules.ActorModule.Scripts.Core.Domain.Repositories
{
    public interface ActorRepository
    {
        IObservable<Unit> Save(Actor actor);
        IObservable<Maybe<Actor>> Get(string playerId);
    }
}