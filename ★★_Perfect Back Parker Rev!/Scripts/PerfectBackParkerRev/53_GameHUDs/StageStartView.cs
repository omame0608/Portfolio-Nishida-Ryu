using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.Databases;
using PerfectBackParkerRev.GameCores.GameSystems.Views;
using PerfectBackParkerRev.GameCores.Users;
using TMPro;
using UnityEngine;
using VContainer;

namespace PerfectBackParkerRev.GameHUDs
{
    /// <summary>
    /// ステージ開始ビュー
    /// </summary>
    public class StageStartView : MonoBehaviour, IStageStartView
    {
        //システム
        [Inject] private readonly IPlayerSystem _playerSystem;

        [Header("操作対象のオブジェクト")]
        [SerializeField] private CanvasGroup _shadow; //影
        [SerializeField] private RectTransform _top; //上帯
        [SerializeField] private RectTransform _bottom; //下帯
        [SerializeField] private RectTransform _stageName; //ステージ名
        [SerializeField] private TMP_Text _stageNameText; //ステージ名テキスト
        [SerializeField] private RectTransform _waveAmount; //ウェーブ数
        [SerializeField] private TMP_Text _waveAmountText; //ウェーブ数テキスト
        [SerializeField] private RectTransform _highScore; //ハイスコア
        [SerializeField] private TMP_Text _highScoreText; //ハイスコアテキスト
        [SerializeField] private GameObject _directionCamera; //演出用カメラ
        [SerializeField] private CanvasGroup _startText; //ステージスタートテキスト
        [SerializeField] private RectTransform[] _startTextParts; //ステージスタートテキストのパーツ
        [SerializeField] private RectTransform[] _startStartBarPart; //ステージスタートバーパーツ

        //Audio
        [Inject] private readonly SEManager _seManager; //SEマネージャー

        //キャンセル用
        private Sequence _showAbstractSequence; //ステージ紹介シーケンス
        private Sequence _startSequence; //スタートシーケンス

        
        public async UniTask ShowStageAbstractView(StageData stageData, int highScore)
        {
            //Viewの初期設定
            var stageName = stageData.StageName;
            stageName += stageData.StageName switch
            {
                "STAGE 1" => "　都市の駐車場",
                "STAGE 2" => "　住宅街の駐車場",
                "STAGE 3" => "　西洋の駐車場",
                "STAGE 4" => "　氷山の駐車場",
                "STAGE 5" => "　火山の駐車場",
                _ => "　テストの駐車場"
            };
            _stageNameText.text = stageName;
            _waveAmountText.text = $"総WAVE数 {stageData.WaveDataList.Count}";
            _highScoreText.text = $"ハイスコア {highScore}";
            var dx = 150f; //各テキストの移動量
            _=_stageName.DOAnchorPosX(-dx, 0f).SetRelative();
            _=_waveAmount.DOAnchorPosX(dx, 0f).SetRelative();
            _=_highScore.DOAnchorPosX(dx, 0f).SetRelative();
            _=_stageNameText.DOFade(0f, 0f);
            _=_waveAmountText.DOFade(0f, 0f);
            _=_highScoreText.DOFade(0f, 0f);

            //アニメーションが残っていた場合キルする
            _showAbstractSequence?.Kill(true);
            _startSequence?.Kill(true);

            //Viewを表示
            gameObject.SetActive(true);

            //SE再生
            _seManager.PlaySE(SE.StageStart);

            //演出シーケンス
            _showAbstractSequence = DOTween.Sequence()
                //カメラ演出を開始
                .AppendCallback(() =>
                {
                    _directionCamera.SetActive(true);
                })
                //一定時間待機
                .AppendInterval(1f)
                //UIをフェードイン
                .Append(_stageName.DOAnchorPosX(dx, 0.5f).SetEase(Ease.OutExpo).SetRelative())
                .Join(_stageNameText.DOFade(1f, 0.5f))
                .Join(_waveAmount.DOAnchorPosX(-dx, 0.5f).SetEase(Ease.OutExpo).SetRelative())
                .Join(_waveAmountText.DOFade(1f, 0.5f))
                .Join(_highScore.DOAnchorPosX(-dx, 0.5f).SetEase(Ease.OutExpo).SetRelative())
                .Join(_highScoreText.DOFade(1f, 0.5f))
                //カメラの演出が終了するまで待機
                .AppendInterval(8f)
                //UIをフェードアウト1
                .Append(_stageName.DOAnchorPosX(dx, 0.5f).SetEase(Ease.OutExpo).SetRelative())
                .Join(_stageNameText.DOFade(0f, 0.5f))
                .Join(_waveAmount.DOAnchorPosX(-dx, 0.5f).SetEase(Ease.OutExpo).SetRelative())
                .Join(_waveAmountText.DOFade(0f, 0.5f))
                .Join(_highScore.DOAnchorPosX(-dx, 0.5f).SetEase(Ease.OutExpo).SetRelative())
                .Join(_highScoreText.DOFade(0f, 0.5f))
                //プレイヤーの演出を再生
                .JoinCallback(() =>
                {
                    _playerSystem.PlayStageStartAnimation();
                    _seManager.PlaySE(SE.PlayerIn);
                })
                //プレイヤーの演出が終了するまで待機
                .AppendInterval(1.9f)
                .AppendCallback(() =>
                {
                    _seManager.StopAllSE();
                    _seManager.PlaySE(SE.PlayerLand);
                })
                .AppendInterval(0.1f)
                //UIをフェードアウト2
                .Append(_top.DOAnchorPosY(200f, 0.2f).SetEase(Ease.OutExpo).SetRelative())
                .Join(_bottom.DOAnchorPosY(-200f, 0.2f).SetEase(Ease.OutExpo).SetRelative())
                .Join(_shadow.DOFade(0f, 0.2f))
                //ウェーブ開始可能状態にする
                .JoinCallback(() =>
                {
                    ShowStageStartText().Forget();
                })
                .AppendCallback(() =>
                {
                    _playerSystem.StopStageStartAnimation();
                    _playerSystem.SwitchRigidbody(true);
                })
                .AppendInterval(1f)
                .AppendCallback(() =>
                {
                    _directionCamera.GetComponentInChildren<Camera>().depth = -1;
                    _directionCamera.SetActive(false);
                })
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                 {
                     _showAbstractSequence = null;
                 })
                //gameobjectにリンク
                .SetLink(gameObject);
            await _showAbstractSequence.AsyncWaitForCompletion().AsUniTask();
        }


        public void CancelShowStageAbstractView()
        {
            //シーケンスをキル
            _showAbstractSequence?.Kill(true);
            _startSequence?.Kill(true);

            //Viewを非表示
            gameObject.SetActive(false);

            //SEを停止
            _seManager.StopAllSE();

            //プレイヤーの演出を停止
            _playerSystem.StopStageStartAnimation();
            _playerSystem.ResetPlayerTransform();

            //カメラ演出を停止
            _directionCamera.GetComponentInChildren<Camera>().depth = -1;
            _directionCamera.SetActive(false);

            //キャンセル用をクリア
            _showAbstractSequence = null;
        }


        public async UniTask ShowStageStartText()
        {
            //Viewの初期設定
            _startText.alpha = 0f;
            foreach (var part in _startTextParts)
            {
                part.anchoredPosition = new Vector2(0f, 0f);
            }
            foreach (var barPart in _startStartBarPart)
            {
                barPart.anchoredPosition += new Vector2(1700f, 0f);
            }

            //アニメーションが残っていたらキル
            _startSequence?.Kill(true);

            //viewを表示
            gameObject.SetActive(true);
            _startText.gameObject.SetActive(true);

            //演出シーケンス
            _startSequence = DOTween.Sequence()
                //テキストをフェードイン
                .Append(_startText.DOFade(1f, 0.1f).SetEase(Ease.OutExpo))
                .JoinCallback(() =>
                {
                    //テキストパーツを展開
                    for (int i = 0; i < _startTextParts.Length; i++)
                    {
                        //移動目標位置を設定
                        var goal = i < 5 ? -500f + i * 100f : -400f + i * 100f;
                        _startTextParts[i].DOAnchorPosX(goal, 1.2f).SetEase(Ease.OutExpo);
                    }
                })
                //バーを展開
                .Insert(0f, _startStartBarPart[0].DOAnchorPosX(-1700f, 0.3f).SetEase(Ease.InExpo).SetRelative())
                .Insert(0f, _startStartBarPart[1].DOAnchorPosX(-1700f, 0.3f).SetEase(Ease.InExpo).SetRelative())
                .Insert(0.15f, _startStartBarPart[2].DOAnchorPosX(-1700f, 0.3f).SetEase(Ease.InExpo).SetRelative())
                .Insert(0.15f, _startStartBarPart[3].DOAnchorPosX(-1700f, 0.3f).SetEase(Ease.InExpo).SetRelative())
                .Insert(0.3f, _startStartBarPart[4].DOAnchorPosX(-1700f, 0.3f).SetEase(Ease.InExpo).SetRelative())
                .Insert(0.3f, _startStartBarPart[5].DOAnchorPosX(-1700f, 0.3f).SetEase(Ease.InExpo).SetRelative())
                //一定時間待機
                .AppendInterval(1.2f)
                //テキストをフェードアウト
                .Append(_startText.DOFade(0f, 0.5f))
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _showAbstractSequence = null;
                })
                //gameobjectにリンク
                .SetLink(gameObject);
            await _startSequence.AsyncWaitForCompletion().AsUniTask();

            //Viewを非表示
            gameObject.SetActive(false);
        }


        //[Title("単体テスト用")]
        //[Button] private void TestShowStageAbstractView(StageData stageData)=>ShowStageAbstractView(stageData).Forget();
    }
}