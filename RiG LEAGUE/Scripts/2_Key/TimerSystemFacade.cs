using VContainer;

public class TimerSystemFacade : ITimerSystemFacade
{
    private readonly TimerSystemMediator _timerSystemMediator;

    public TimerSystemFacade(TimerSystemMediator timerSystemMediator)
    {
        /* Mediatorを注入する */
        _timerSystemMediator = timerSystemMediator;
    }

    public void AddTimerObserver(ITimerObserver timerObserver)
    {
        _timerSystemMediator.AddTimerObserver(timerObserver);
    }

    public void RemoveTimerObserver(ITimerObserver timerObserver)
    {
        _timerSystemMediator.RemoveTimerObserver(timerObserver);
    }

    public void UseTimer(int time)
    {
        _timerSystemMediator.UseTimer(time);
    }
}
