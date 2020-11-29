using System;
using Modules.UlaGame.Scripts.Core.Domain;
using UniRx;

namespace Modules.UlaGame.Scripts.Core.Actions
{
    public class StartUlaGameAction
    {
        private readonly UlaGameEventBus eventBus;
        
        public StartUlaGameAction() { }

        public StartUlaGameAction(UlaGameEventBus eventBus)
        {
            this.eventBus = eventBus;
        }
        public virtual void Execute()
        {
            new Domain.UlaGame(eventBus,
                Observable.Interval(TimeSpan.FromSeconds(10)),
                Observable.Interval(TimeSpan.FromSeconds(1)),
                0.5f,
                30f);
        }
    }
}