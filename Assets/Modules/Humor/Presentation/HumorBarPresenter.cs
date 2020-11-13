using System;
using System.Collections.Generic;
using Modules.Actor.Scripts.Core;
using Modules.Actor.Scripts.Core.Domain.Action;
using Modules.Actor.Scripts.Core.Domain.Events;
using UniRx;

namespace Modules.Humor.Presentation
{
    public class HumorBarPresenter
    {
        private readonly HumorBarView view;
        private readonly EventBus actorEventBus;
        private readonly RetrieveActorHumor retrieveHumor;
        
        private List<IDisposable> disposer = new List<IDisposable>();

        public HumorBarPresenter(HumorBarView view, EventBus actorEventBus, RetrieveActorHumor retrieveHumor)
        {
            this.view = view;
            this.actorEventBus = actorEventBus;
            this.retrieveHumor = retrieveHumor;

            view.OnViewEnable += PrepareView;
            view.OnViewDisable += DisposeView;
        }

        private void DisposeView()
        {
            disposer.ForEach(d => d.Dispose());
        }

        private void PrepareView()
        {
            actorEventBus.OnEvent<HumorChangesEvent>()
                .Do(humorState =>
                {
                    var humor = retrieveHumor.Execute();
                    view.HumorChange(humor.humorLevel, humor.lastHumorChange);
                })
                .Subscribe()
                .AddTo(disposer);
        }
    }
}