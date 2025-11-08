using Alchemy.Inspector;
using DG.Tweening;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.GameCores.GameSystems.Views;
using PerfectBackParkerRev.Utilities;
using TMPro;
using UnityEngine;
using VContainer;

namespace PerfectBackParkerRev.GameHUDs
{
    /// <summary>
    /// ウェーブタイマーのビュー
    /// </summary>
    public class WaveTimerView : MonoBehaviour, IWaveTimerView
    {
        [Header("操作対象のオブジェクト")]
        [SerializeField] private TMP_Text _waveTimerText; //タイマー
        [SerializeField] private RectTransform _waveTimer; //タイマー

        //Audio
        [Inject] private SEManager _seManager; //SEマネージャー

        //キャンセル用
        private Sequence _sequence;


        public void ShowWaveTimer()
        {
            //アニメーションが残っていた場合キルする
            _sequence?.Kill(true);

            //初期設定
            _waveTimerText.color = Color.white;

            //Viewを表示
            gameObject.SetActive(true);

            //演出シーケンス
            _sequence = DOTween.Sequence()
                //カウント開始直後は拡大して表示
                .Append(_waveTimer.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.5f).SetEase(Ease.InOutQuint))
                .AppendInterval(2f)
                .Append(_waveTimer.DOScale(new Vector3(1f, 1f, 1f), 1f).SetEase(Ease.InOutQuint))
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                 {
                     _sequence = null;
                 });
        }


        public void HideWaveTimer()
        {
            //アニメーションが残っていた場合キルする
            _sequence?.Kill(true);

            //Viewを非表示
            gameObject.SetActive(false);
        }


        public void UpdateWaveTimer(int time)
        {
            if (time < 0)
            {
                MyLogger.LogError($"[WaveTimerView]負の秒数は表示できません。");
                return;
            }

            //Viewを更新
            _waveTimerText.text = $"残り：{time}秒";

            //SE再生
            if (time % 2 == 0) _seManager.PlaySE(SE.Timer1);
            else _seManager.PlaySE(SE.Timer2);

            //特定の時間への更新なら警告アニメーションを再生
            switch (time)
            {
                case 30:
                    _seManager.PlaySE(SE.Alert); //警告音再生
                    _waveTimer.DOShakeAnchorPos(1f, 40f);
                    _waveTimerText.DOColor(Color.yellow, 0.2f);
                    break;
                case 10:
                    _seManager.PlaySE(SE.Alert); //警告音再生
                    _waveTimer.DOShakeAnchorPos(1f, 40f);
                    _waveTimerText.DOColor(Color.red, 0.2f);
                    break;
                case 3:
                    _waveTimer.DOShakeAnchorPos(0.5f, 40f);
                    break;
                case 2:
                    _waveTimer.DOShakeAnchorPos(0.5f, 60f);
                    break;
                case 1:
                    _waveTimer.DOShakeAnchorPos(0.5f, 80f);
                    break;
                case 0:
                    _waveTimer.DOShakeAnchorPos(0.5f, 100f);
                    break;
            }
        }


        [Title("単体テスト用")]
        [Button] private void TestShowWaveTimer() => ShowWaveTimer();
        [Button] private void TestHideWaveTimer() => HideWaveTimer();
        int test = 60;
        [Button] private void TestUpdateWaveTimer() => UpdateWaveTimer(test--);
    }
}