using Alchemy.Inspector;
using DG.Tweening;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.Databases;
using PerfectBackParkerRev.GameCores.GameSystems.Views;
using TMPro;
using UnityEngine;
using VContainer;

namespace PerfectBackParkerRev.GameHUDs
{
    /// <summary>
    /// ウェーブ開始時のビュー
    /// </summary>
    public class WaveStartView : MonoBehaviour, IWaveStartView
    {
        [Header("操作対象のオブジェクト")]
        [SerializeField] private TMP_Text _waveNumberText; //ウェーブ番号テキスト
        [SerializeField] private TMP_Text _timelimitText; //制限時間テキスト
        [SerializeField] private TMP_Text _overViewText; //概要テキスト
        [SerializeField] private RectTransform _topLine; //上ライン
        [SerializeField] private RectTransform _middleLine; //中ライン
        [SerializeField] private RectTransform _bottomLine; //下ライン

        //Audio
        [Inject] private SEManager _seManager; //SEマネージャー

        //キャンセル用
        private Sequence _sequence;


        public void ShowWaveInfomation(WaveData waveData)
        {
            //Viewの初期設定
            _waveNumberText.text = $"{waveData.WaveName}";
            _timelimitText.text = $"制限時間：{waveData.TimeLimit}秒";
            _overViewText.text = $"ウェーブ基礎スコア：{waveData.WaveBaseScore}点";
            var firstPosX = -1050f; //初期位置X座標
            _topLine.anchoredPosition = new Vector2(firstPosX, _topLine.anchoredPosition.y);
            _middleLine.anchoredPosition = new Vector2(firstPosX, _middleLine.anchoredPosition.y);
            _bottomLine.anchoredPosition = new Vector2(firstPosX, _bottomLine.anchoredPosition.y);

            //アニメーションが残っていた場合キルする
            _sequence?.Kill(true);

            //Viewを表示
            gameObject.SetActive(true);

            //SE再生
            _seManager.PlaySE(SE.Klaxon);

            //演出シーケンス
            _sequence = DOTween.Sequence()
                //各UIを画面内に移動
                .Insert(0f, _topLine.DOAnchorPosX(0f, 1f).SetEase(Ease.InOutElastic))
                .Insert(0.2f, _middleLine.DOAnchorPosX(0f, 1f).SetEase(Ease.InOutElastic))
                .Insert(0.4f, _bottomLine.DOAnchorPosX(0f, 1f).SetEase(Ease.InOutElastic))
                //各UIを画面外に移動
                .Insert(5f, _topLine.DOAnchorPosX(-1050f, 1f).SetEase(Ease.InOutElastic))
                .Insert(5.2f, _middleLine.DOAnchorPosX(-1050f, 1f).SetEase(Ease.InOutElastic))
                .Insert(5.4f, _bottomLine.DOAnchorPosX(-1050f, 1f).SetEase(Ease.InOutElastic))
                //終了時にViewを非表示にする
                .AppendCallback(() => gameObject.SetActive(false))
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                 {
                     _sequence = null;
                 });
        }


        [Title("単体テスト用")]
        [Button] private void TestShowStageInfomation(WaveData waveData) => ShowWaveInfomation(waveData);
    }
}