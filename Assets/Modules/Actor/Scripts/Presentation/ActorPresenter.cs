using System;
using System.Collections.Generic;
using Modules.Actor.Scripts.Core;
using Modules.Actor.Scripts.Presentation;
using UniRx;
using UnityEditor.SceneManagement;


public class ActorPresenter
{ 
    readonly ActorView view;
    readonly EventBus eventBus;
    private readonly List<IDisposable> disposables = new List<IDisposable>();

    public ActorPresenter(ActorView view, EventBus eventBus)
    {
        this.view = view;
        this.eventBus = eventBus;

        view.OnViewEnable += Present;
        view.OnViewDisable += Remove;
    }

    void Present()
    {
        eventBus.OnCaressEvent()
            .Do(_ => view.ShowCaredFeedback())
            .Subscribe()
            .AddTo(disposables);
        
        eventBus.OnNotHappyEvent()
            .Do(_ => view.ShowNotHappyFeedback())
            .Subscribe()
            .AddTo(disposables);
    }

    void Remove()
    {
        disposables.ForEach(d => d.Dispose());
    }
    
}
