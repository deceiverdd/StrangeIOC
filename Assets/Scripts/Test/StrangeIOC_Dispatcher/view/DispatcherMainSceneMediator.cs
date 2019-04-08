using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using System;

public class DispatcherMainSceneMediator : EventMediator
{
    [Inject]
    public DispatcherMainSceneView view { get; set; }

    public override void PreRegister()
    {

    }

    public override void OnRegister()
    {
        //Listen to the view for an event
        view.dispatcher.AddListener(DispatcherMainSceneView.CLICK_STARTGAME_EVENT, OnStartGame);

        //Listen to the global event bus for events
        dispatcher.AddListener(DispatcherGlobalEvent.EXITGAME, OnExitGame);

        view.Init();
    }

    public override void OnRemove()
    {
        //Clean up listener when the view is about to be destroyed
        view.dispatcher.RemoveListener(DispatcherMainSceneView.CLICK_STARTGAME_EVENT, OnStartGame);
        dispatcher.RemoveListener(DispatcherGlobalEvent.EXITGAME, OnExitGame);
    }

    private void OnExitGame()
    {
        Application.Quit();
    }

    private void OnStartGame()
    {
        dispatcher.Dispatch(DispatcherGlobalEvent.STARTGAME);
    }
}
