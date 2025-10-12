using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// タイマーのアニメーションを担うクラス
/// </summary>
public class TimerViewAnimator
{
    private readonly TimerView _timerView;
    private int _limitTime = 0;
    public TimerViewAnimator(TimerView timerView) {
        _timerView = timerView;
    }

    /// <summary>
    /// 初期化を行う
    /// </summary>
    /// <param name="time">残り時間</param>
    public void Initialize(int time) {
        _timerView.TimerProgressBar.fillAmount = 1.0f;
        _limitTime = time;
    }

    /// <summary>
    /// プログレスバーのアニメーションを行う
    /// </summary>
    public void AnimateProgressBar() {
        _timerView.TimerProgressBar
            .DOFillAmount(0.0f, _limitTime)
            .SetEase(Ease.Linear)
            .SetLink(_timerView.gameObject);
    }
}
