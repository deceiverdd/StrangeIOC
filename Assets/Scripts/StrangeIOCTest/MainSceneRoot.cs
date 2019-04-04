using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.impl;

public class MainSceneRoot : ContextView
{
    private void Awake()
    {
        context = new MainSceneContext(this);
    }
}
