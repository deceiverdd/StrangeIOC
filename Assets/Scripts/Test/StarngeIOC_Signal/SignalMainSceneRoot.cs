using strange.extensions.context.impl;

public class SignalMainSceneRoot : ContextView
{
    private void Awake()
    {
        context = new SignalMainSceneContext(this);
    }
}
