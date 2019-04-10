using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;

public class DAStartCommand : Command
{
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject ContextView { get; set; }

    public override void Execute()
    {
        GameObject go = new GameObject
        {
            name = "DAMainView"
        };

        go.AddComponent<DAMainView>();
        go.transform.parent = ContextView.transform;
    }
}
