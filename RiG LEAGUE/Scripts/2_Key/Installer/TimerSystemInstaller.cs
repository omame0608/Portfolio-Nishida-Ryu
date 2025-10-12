using VContainer;
using VContainer.Unity;

public class TimerSystemInstaller
{
    /// <summary>
    /// TimerSystemのDI登録をする
    /// </summary>
    /// <param name="builder"></param>
    public static void Install(IContainerBuilder builder) 
    {
        builder.RegisterComponentInHierarchy<TimerView>();
        builder.Register<TimerSystemMediator>(Lifetime.Singleton);
        builder.Register<TimerSystemFacade>(Lifetime.Singleton);
        builder.Register<TimerViewAnimator>(Lifetime.Singleton);
    }
}
