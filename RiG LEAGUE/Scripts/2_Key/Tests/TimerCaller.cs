using UniRx;
using UnityEngine;
using VContainer;

/// <summary>
/// タイマー動作テストクラス
/// </summary>
public class TimerCaller : MonoBehaviour
{
    private TimerSystemFacade _timerSystemFacade;
    
    [SerializeField, Header("タイマーの制限時間")]
    private int _limitTime = 10;

    /// <summary>
    /// Facadeを注入する
    /// </summary>
    /// <param name="timerSystemFacade"></param>
    [Inject]
    public void Construct(TimerSystemFacade timerSystemFacade)
    {
        _timerSystemFacade = timerSystemFacade;
    }

    // Start is called before the first frame update
    void Start()
    {
        _timerSystemFacade.UseTimer(_limitTime);

        Observable.EveryUpdate()
            .TakeUntilDestroy(this)
            .Where(_ => Input.GetKeyDown(KeyCode.Space))
            .Subscribe(_ => _timerSystemFacade.UseTimer(_limitTime))
            .AddTo(this);
    }
}
