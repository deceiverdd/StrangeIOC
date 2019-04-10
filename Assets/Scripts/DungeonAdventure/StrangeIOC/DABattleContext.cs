using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using UnityEngine;

public class DABattleContext : MVCSContext
{
    public DABattleContext(MonoBehaviour view) : base(view)
    {

    }

    public DABattleContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {

    }

    // Unbind the default EventCommandBinder and rebind the SignalCommandBinder
    protected override void addCoreComponents()
    {
        base.addCoreComponents();
        injectionBinder.Unbind<ICommandBinder>();
        injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
    }

    // Override Start so that we can fire the StartSignal 
    public override IContext Start()
    {
        base.Start();
        DAStartSignal startSignal = (DAStartSignal)injectionBinder.GetInstance<DAStartSignal>();
        startSignal.Dispatch();
        return this;
    }

    protected override void mapBindings()
    {
        injectionBinder.Bind<IPlayerModel>().To<PlayerModel>().ToSingleton();
        injectionBinder.Bind<PlayerModel>().To<DAPlayerCtr>().ToSingleton();

        mediationBinder.Bind<DAMainView>().To<DAMainViewMediator>();

        commandBinder.Bind<DAStartSignal>().To<DAStartCommand>().Once();
    }
}
