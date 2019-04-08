using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;

public class SignalStartCommand : Command
{
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject contextView { get; set; }

    public override void Execute()
    {
        Transform canvas = contextView.transform.Find("Canvas");
        GameObject go = Resources.Load("Signal_MainScene", typeof(GameObject)) as GameObject;
        GameObject MainSceneUI = GameObject.Instantiate(go);
        MainSceneUI.name = "MainSceneView";
        MainSceneUI.AddComponent<SignalMainSceneView>();
        MainSceneUI.transform.SetParent(canvas, false);
    }
}
