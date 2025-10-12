using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// タイマーの表示部分を担うクラス
/// </summary>
public class TimerView : MonoBehaviour, ITimerCountDownObserver
{
    [SerializeField, Header("残り時間を表示するテキスト")]
    private TMP_Text _timerText;
    public TMP_Text TimerText => _timerText;

    [SerializeField, Header("残り時間を表示するプログレスバー")]
    private Image _timerProgressBar;
    public Image TimerProgressBar => _timerProgressBar;
    
    [SerializeField, Header("ワーニング表示をする残り時間")]
    private int _warningTime = 5;

    /// <summary>
    /// 初期化を行う
    /// </summary>
    /// <param name="time">残り時間</param>
    public void Initialize(int time)
    {
        _timerText.color = Color.black;
        UpdateView(time);
    }

    public void OnCountDown(int time)
    {
        UpdateView(time);
    }

    /// <summary>
    /// テキストを更新する
    /// </summary>
    /// <param name="time">残り時間</param>
    public void UpdateView(int time)
    {
        if (time <= _warningTime)
            _timerText.color = Color.red;
        
        _timerText.text = time.ToString();
    }
}