using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using System;

public class MainSceneMediator : EventMediator
{
    [Inject]
    public MainSceneView view { get; set; }

    public override void PreRegister()
    {

    }

    public override void OnRegister()
    {
        //Listen to the view for an event
        view.dispatcher.AddListener(MainSceneView.CLICK_STARTGAME_EVENT, OnStartGame);

        //Listen to the global event bus for events
        dispatcher.AddListener(GlobalEvent.EXITGAME, OnExitGame);

        view.Init();
    }

    public override void OnRemove()
    {
        //Clean up listener when the view is about to be destroyed
        view.dispatcher.RemoveListener(MainSceneView.CLICK_STARTGAME_EVENT, OnStartGame);
        dispatcher.RemoveListener(GlobalEvent.EXITGAME, OnExitGame);
    }

    private void OnExitGame()
    {
        Application.Quit();
    }

    private void OnStartGame()
    {
        dispatcher.Dispatch(GlobalEvent.STARTGAME);
    }
}
