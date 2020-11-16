using System;
using System.Collections.Generic;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Action;
using Modules.ActorModule.Scripts.Core.Domain.Events;
using Modules.Common;
using UniRx;

namespace Modules.ActorModule.Scripts.Presentation
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
            disposer.DisposeAll();
        }

        private void PrepareView()
        {
            retrieveHumor.Execute()
                .Do(actualHumor =>
                {
                    view.InitView(actualHumor.humorLevel, actualHumor.maxHumor);
                    actorEventBus.OnEvent<HumorChangesEvent>().Do(humorState =>
                            {
                                retrieveHumor.Execute().Do(humor =>
                                    {
                                        view.HumorChange(humor.humorLevel, humor.lastHumorChange, humor.maxHumor);
                                    });
                            })
                        .Subscribe()
                        .AddTo(disposer);
                });

        }
    }
}