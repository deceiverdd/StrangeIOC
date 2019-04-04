using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;

public class MainSceneContext : MVCSContext
{
    public MainSceneContext(MonoBehaviour view) : base(view)
    {

    }

    public MainSceneContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {

    }

    protected override void mapBindings()
    {
        mediationBinder.Bind<MainSceneView>().To<MainSceneMediator>();

        commandBinder.Bind(GlobalEvent.STARTGAME).To<StartGameCommand>();

        commandBinder.Bind(ContextEvent.START).To<StartCommand>().Once();
    }
}
