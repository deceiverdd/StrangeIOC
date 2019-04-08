using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;
using strange.extensions.context.api;

public class DispatcherStartGameCommand : EventCommand
{
    public override void Execute()
    {
        SceneMgr.Instance.LoadScene(SceneName.Battle_0);
    }
}
