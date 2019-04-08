using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.impl;

public class DispatcherMainSceneRoot : ContextView
{
    private void Awake()
    {
        context = new DispatcherMainSceneContext(this);
    }
}
