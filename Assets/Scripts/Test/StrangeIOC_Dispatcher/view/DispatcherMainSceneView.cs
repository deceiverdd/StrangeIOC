using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine.UI;

public class DispatcherMainSceneView : View
{
    private Button BtnStartGame;
    private Button BtnExitGame;

    internal const string CLICK_STARTGAME_EVENT = "CLICK_STARTGAME_EVENT";

    [Inject]
    public IEventDispatcher dispatcher { get; set; }

    public void Init()
    {
        RegisterBtn();
    }

    private void RegisterBtn()
    {
        BtnStartGame = FindBtn("BtnStartGame");
        BtnExitGame = FindBtn("BtnExitGame");

        EventTriggerListener.Get(BtnStartGame.gameObject).onClick = OnButtonClick;
        EventTriggerListener.Get(BtnExitGame.gameObject).onClick = OnButtonClick;
    }

    private void OnButtonClick(GameObject btn)
    {
        if(btn == BtnStartGame.gameObject)
        {
            dispatcher.Dispatch(CLICK_STARTGAME_EVENT);
        }
        else if(btn == BtnExitGame.gameObject)
        {
            dispatcher.Dispatch(DispatcherGlobalEvent.EXITGAME);
        }
    }
}
