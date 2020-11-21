using System;
using System.Collections.Generic;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Action;
using Modules.ActorModule.Scripts.Core.Domain.Events;
using Modules.ActorModule.Scripts.Presentation.Events;
using Modules.Common;
using UniRx;

namespace Modules.ActorModule.Scripts.Presentation
{
    public class ActorAvatarPresenter
    {
        private ActorAvatarView view;
        private RetrieveActor retrieveActor;
        private EventBus eventBus;
        private RetrieveActorHumor retrieveHumor;
        
        private List<IDisposable> disposer = new List<IDisposable>();

        public ActorAvatarPresenter(ActorAvatarView view,
            RetrieveActor retrieveActor,
            RetrieveActorHumor retrieveHumor,
            EventBus eventBus)
        {
            this.view = view;
            this.retrieveActor = retrieveActor;
            this.eventBus = eventBus;
            this.retrieveHumor = retrieveHumor;
            view.OnViewEnabled += PresentView;
            view.OnViewDisabled += DisposeView;
        }

        private void PresentView()
        {
            eventBus.OnEvent<HumorChangesEvent>()
                .Do(_ => HandleHumorChange())
                .Subscribe()
                .AddTo(disposer);
            
            retrieveActor.Execute()
                .Do(actor => view.SetName(actor.name));

            retrieveHumor.Execute()
                .Do(humor => view.SetHumor(humor.humor));
        }
        

        private void DisposeView()
        {
            disposer.DisposeAll();
        }

        private void HandleHumorChange()
        {
            retrieveHumor.Execute()
                .Do(humor => view.SetHumor(humor.humor));
        }
    }
}