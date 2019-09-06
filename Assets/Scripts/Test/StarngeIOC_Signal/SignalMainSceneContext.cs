using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;

public class SignalMainSceneContext : MVCSContext
{
    public SignalMainSceneContext(MonoBehaviour view) : base(view)
    {

    }

    public SignalMainSceneContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {

    }

    protected override void addCoreComponents()
    {
        base.addCoreComponents();
        injectionBinder.Unbind<ICommandBinder>();
        injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
    }

    public override IContext Start()
    {
        base.Start();
        SignalStartSignal startSignal = injectionBinder.GetInstance<SignalStartSignal>();
        startSignal.Dispatch();
        return this;
    }

    protected override void mapBindings()
    {
        //Bind Model
        
        //Bind Mediator for each View
        mediationBinder.Bind<SignalMainSceneView>().To<SignalMainSceneMediator>();
        commandBinder.Bind<SignalStartSignal>().To<SignalStartCommand>().Once();
        injectionBinder.Bind<SignalScoreChangeSignal>().ToSingleton();
    }
}

