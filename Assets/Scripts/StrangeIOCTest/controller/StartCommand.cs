using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;

public class StartCommand : EventCommand
{
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject contextView { get; set; }

    public override void Execute()
    {
        Transform canvas = contextView.transform.Find("Canvas");

        GameObject go = Resources.Load("MainScene", typeof(GameObject)) as GameObject;
        GameObject mainSceneUI = GameObject.Instantiate(go) as GameObject;
        mainSceneUI.name = "MainSceneView";
        mainSceneUI.AddComponent<MainSceneView>();
        mainSceneUI.transform.SetParent(canvas, false);
    }
}
