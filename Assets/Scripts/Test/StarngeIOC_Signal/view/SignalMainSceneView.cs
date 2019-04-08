using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.signal.impl;
using strange.extensions.mediation.impl;
using UnityEngine.UI;

public class SignalMainSceneView : View
{
    public Button BtnStartGame;
    public Button BtnExitGame;

    public Signal<string> BtnClickSignal = new Signal<string>();

    internal void Init()
    {
        RegisterBtn();
    }

    internal void UpdateScore(string score)
    {

    }

    private void RegisterBtn()
    {
        BtnStartGame = FindBtn("BtnStartGame");
        EventTriggerListener.Get(BtnStartGame.gameObject).onClick = OnButtonClick;

        BtnExitGame = FindBtn("BtnExitGame");
        EventTriggerListener.Get(BtnExitGame.gameObject).onClick = OnButtonClick;
    }

    private void OnButtonClick(GameObject btn)
    {
        if (btn == BtnStartGame.gameObject)
        {
            BtnClickSignal.Dispatch("BtnStartGame");
        }
        else if (btn == BtnExitGame.gameObject)
        {
            BtnClickSignal.Dispatch("BtnExitGame");
        }
    }
}
