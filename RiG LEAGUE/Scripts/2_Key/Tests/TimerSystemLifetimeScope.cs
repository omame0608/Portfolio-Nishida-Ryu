using VContainer;
using VContainer.Unity;

/// <summary>
/// タイマーシステムの依存関係を登録する
/// </summary>
public class TimerSystemLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        //builder.RegisterComponentInHierarchy<TimerCaller>();
        TimerSystemInstaller.Install(builder);
    }
}
