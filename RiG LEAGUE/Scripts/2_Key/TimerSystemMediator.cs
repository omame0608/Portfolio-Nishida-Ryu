using UnityEngine;
using VContainer;

/// <summary>
/// タイマー空間の仲介を担う
/// </summary>
public class TimerSystemMediator : ITimerObserver
{
    private readonly TimerView _timerSystemView;
    private readonly TimerViewAnimator _timerViewAnimator;
    private TimerCounter _timerCounter = null;
    
    public TimerSystemMediator(TimerView timerSystemView, TimerViewAnimator timerViewAnimator)
    {
        _timerSystemView = timerSystemView;
        _timerViewAnimator = timerViewAnimator;
    }

    public void AddTimerObserver(ITimerObserver timerObserver)
    {
        /* タイマーが起動していない場合 */
        if (_timerCounter == null)
        {
            Debug.LogError("タイマーが起動していないため、アクションを登録出来ません");
            return;
        }

        _timerCounter.AddTimerObserver(timerObserver);
    }

    public void RemoveTimerObserver(ITimerObserver timerObserver)
    {
        /* タイマーが起動していない場合 */
        if (_timerCounter == null)
        {
            Debug.LogError("タイマーが起動していないため、アクションを削除出来ません");
            return;
        }

        _timerCounter.RemoveTimerObserver(timerObserver);
    }

    public void UseTimer(int time)
    {
        /* タイマーが起動中の場合 */
        if (_timerCounter != null)
        {
            Debug.LogError("既にタイマーが起動しています");
            return;
        }

        _timerCounter = new TimerCounter(time);
        _timerSystemView.Initialize(time);
        _timerViewAnimator.Initialize(time);
        _timerViewAnimator.AnimateProgressBar();

        /* カウントダウンに応じてテキストが更新されるようにする */
        _timerCounter.AddCountDownObserver(_timerSystemView);

        /* タイムアップ時に通知を受け取る */
        _timerCounter.AddTimerObserver(this);
    }

    public void OnTimeUp()
    {
        _timerCounter = null;
        Debug.Log("タイマーが終了しました");
    }
}