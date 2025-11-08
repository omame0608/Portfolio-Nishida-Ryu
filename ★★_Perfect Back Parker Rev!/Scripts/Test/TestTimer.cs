using PerfectBackParkerRev.GameCores.GameSystems;
using UniRx;
using UnityEngine;
using VContainer;

namespace Test
{
    /// <summary>
    /// テスト用：タイマーの動作確認用ドライバ
    /// </summary>
    public class TestTimer : MonoBehaviour
    {
        // タイマー
        //private readonly CountdownTimer _timer = new();
        [Inject] private WaveTimer _timer;


        private void Start()
        {
            //タイマーへの購読設定
            //_timer.OnTick.Subscribe(time => Debug.Log($"残り時間: {time}秒"));
            _timer.OnWaveFinishDetected.Subscribe(_ => Debug.Log("カウントダウン完了！"));
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                //タイマー開始、ゲーム時間準拠
                _timer.StartWaveTimer(5);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                //タイマーキャンセル
                _timer.ResetWaveTimer();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                //タイマー破棄
                //_timer.Dispose();
            }
        }
    }
}