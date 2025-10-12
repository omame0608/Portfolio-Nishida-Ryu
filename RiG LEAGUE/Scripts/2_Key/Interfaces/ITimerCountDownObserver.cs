/// <summary>
/// タイマーのカウントダウンに対するアクションができるオブジェクト
/// </summary>
public interface ITimerCountDownObserver
{
    /// <summary>
    /// カウントダウンに対するアクション
    /// </summary>
    /// <param name="time">残り時間</param>
    public void OnCountDown(int time);
}