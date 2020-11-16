using System;
using System.Collections.Generic;
using Modules.ActorModule.Scripts.Core.Domain;
using Modules.ActorModule.Scripts.Core.Domain.Action;
using Modules.ActorModule.Scripts.Core.Domain.Events;
using Modules.Common;
using UniRx;

namespace Modules.ActorModule.Scripts.Presentation
{
    public class ActorPresenter
    { 
        readonly ActorView view;
        readonly EventBus eventBus;
        private readonly RetrieveActorHumor retrieveHumor;
        private readonly List<IDisposable> disposables = new List<IDisposable>();

        public ActorPresenter(ActorView view, EventBus eventBus, RetrieveActorHumor retrieveHumor)
        {
            this.view = view;
            this.eventBus = eventBus;
            this.retrieveHumor = retrieveHumor;

            view.OnViewEnable += Present;
            view.OnViewDisable += Remove;
        }

        void Present()
        {
            eventBus.OnEvent<LeftCaressInteractionEvent>()
                .Do(_ => view.ShowLeftCaredFeedback())
                .Subscribe()
                .AddTo(disposables);
        
            eventBus.OnEvent<RigthCaressInteractionEvent>()
                .Do(_ => view.ShowRigthCaredFeedback())
                .Subscribe()
                .AddTo(disposables);
        
            eventBus.OnEvent<HappyEvent>()
                .Do(_ => view.ShowHappyFeedback())
                .Subscribe()
                .AddTo(disposables);
        
            eventBus.OnEvent<NotHappyEvent>()
                .Do(_ => view.ShowNotHappyFeedback())
                .Subscribe()
                .AddTo(disposables);

            eventBus.OnEvent<HumorChangesEvent>()
                .Do(_ => HandleHumorChange())
                .Subscribe()
                .AddTo(disposables);
        
            eventBus.OnEvent<LeftTickleInteractionEvent>()
                .Do(_ => view.ShowLeftTickleFeedback())
                .Subscribe()
                .AddTo(disposables);
        
            eventBus.OnEvent<RightTickleInteractionEvent>()
                .Do(_ => view.ShowRightTickleFeedback())
                .Subscribe()
                .AddTo(disposables);
        
            eventBus.OnEvent<MiddleConsentEvent>()
                .Do(_ => view.ShowMiddleConsentEvent())
                .Subscribe()
                .AddTo(disposables);
        }

        private void HandleHumorChange()
        {
            retrieveHumor.Execute()
                .Do(humorState =>
                {
                    if (Humor.Normal.Equals(humorState.humor)) view.ShowNormalIdle();
                    if (Humor.Happy.Equals(humorState.humor)) view.ShowHappyIdle();
                    if (Humor.NotHappy.Equals(humorState.humor)) view.ShowNotHappyIdle();
                });
        }

        void Remove()
        {
            disposables.DisposeAll();
        }
    
    }
}
