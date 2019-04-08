using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;

public class SignalMainSceneMediator : Mediator
{
    [Inject]
    public SignalMainSceneView view { get; set; }

    [Inject]
    public SignalScoreChangeSignal scoreChangedSignal { get; set; }

    public override void OnRegister()
    {
        scoreChangedSignal.AddListener(OnScoreChange);
        view.BtnClickSignal.AddListener(OnBtnClick);

        view.Init();
    }

    public override void OnRemove()
    {
        scoreChangedSignal.RemoveListener(OnScoreChange);
        view.BtnClickSignal.RemoveListener(OnBtnClick);
    }

    private void OnBtnClick(string btn)
    {
        Debuger.Log(btn);
    }

    private void OnScoreChange(string score)
    {
        view.UpdateScore(score);
        view.Init();
    }
}
