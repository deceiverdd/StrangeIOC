using strange.extensions.context.impl;

public class DABattleRoot : ContextView
{
    private void Awake()
    {
        context = new DABattleContext(this);
    }
}
