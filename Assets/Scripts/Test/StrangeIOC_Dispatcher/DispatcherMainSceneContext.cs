using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;

public class DispatcherMainSceneContext : MVCSContext
{
    public DispatcherMainSceneContext(MonoBehaviour view) : base(view)
    {

    }

    public DispatcherMainSceneContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {

    }

    protected override void mapBindings()
    {
        mediationBinder.Bind<DispatcherMainSceneView>().To<DispatcherMainSceneMediator>();

        commandBinder.Bind(DispatcherGlobalEvent.STARTGAME).To<DispatcherStartGameCommand>();

        commandBinder.Bind(ContextEvent.START).To<DispatcherStartCommand>().Once();
    }
}
