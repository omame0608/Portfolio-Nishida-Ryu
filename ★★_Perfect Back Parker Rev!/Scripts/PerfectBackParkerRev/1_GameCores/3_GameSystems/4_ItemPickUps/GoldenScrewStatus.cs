using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.Utilities;
using System;
using UniRx;
using VContainer;

namespace PerfectBackParkerRev.GameCores.GameSystems.ItemPickUps
{
    /// <summary>
    /// 「金のネジ」の取得情報を保持する
    /// ウェーブをクリアしたときのみ取得情報を更新することに注意
    /// （金のネジを取ってもゴール出来なければGETにはならない）
    /// </summary>
    public class GoldenScrewStatus : IDisposable, IGoldenScrewGetNotifierRegistrar
    {
        //Audio
        [Inject] private readonly SEManager _seManager; //SEマネージャー

        //検出対象の「金のネジ」
        //動的に対象が変化すること、更新後に再講読しないと適用はされないことに注意
        private IGoldenScrewGetNotifier _goldenScrewDetector;

        //購読解除用
        private readonly CompositeDisposable _disposables = new();

        //各ウェーブの「金のネジ」取得状況配列
        public bool?[] _goldenScrewStatusArray;

        //取得状況を暫定保持
        private bool _getGoldenScrewWithoutClear = false; //クリアが確定したら上記配列に記録する
        private int _currentWaveIndex; //現在のウェーブを格納する添え字


        public void AddNotifier(IGoldenScrewGetNotifier notifier)
        {
            //通知者が登録されていなければ登録
            if (_goldenScrewDetector != null)
            {
                MyLogger.LogError("[GoldenScrewStatus]既に「金のネジ」検出器が登録されています");
                return;
            }
            _goldenScrewDetector = notifier;
        }


        /// <summary>
        /// リストの作成などの初期化処理
        /// StartSequenceで呼ばれる想定
        /// </summary>
        /// <param name="waveCount">管理するステージの総ステージ数</param>
        public void InitStatus(int waveCount)
        {
            //取得状況配列を初期化
            _goldenScrewStatusArray = new bool?[waveCount];
        }


        /// <summary>
        /// 「金のネジ」取得の監視を適用し、暫定の所持状況を記録
        /// ウェーブ開始時の呼ばれる想定
        /// </summary>
        /// <param name="currentWaveName">現在進行中のウェーブ名</param>
        public void StartObserve(string currentWaveName)
        {
            //ウェーブ名を配列の添え字に加工
            if (!int.TryParse(currentWaveName.Replace("WAVE ", ""), out int waveNumber))
            {
                //変換失敗エラー
                MyLogger.LogError($"[GoldenScrewStatus]WAVE名をindexに変換できませんでした");
            }
            _currentWaveIndex = waveNumber - 1;

            //暫定保持状況を初期化
            _getGoldenScrewWithoutClear = false;

            //オブジェクト監視を開始
            _goldenScrewDetector.OnGoldenScrewGetDetected
                .Subscribe(_ =>
                {
                    _getGoldenScrewWithoutClear = true;
                    //SE再生
                    _seManager.PlaySE(SE.ScrewPickup);
                })
                .AddTo(_disposables);
        }


        /// <summary>
        /// 「金のネジ」取得の監視を解除
        /// ウェーブクリアであれば暫定所持状況をリストへ書き込み
        /// ウェーブ終了時の呼ばれる想定
        /// </summary>
        /// <param name="isWaveClear">ウェーブをクリアしたかどうか</param>
        /// <returns>金のネジを取得したかどうか、またはウェーブ失敗によりnullか</returns>
        public bool? FinishObserve(bool isWaveClear)
        {
            //購読解除用
            _disposables.Clear();

            //検出対象を初期化
            _goldenScrewDetector = null;

            //ウェーブをクリアしていた場合
            if (isWaveClear)
            {
                //暫定取得情報を正式に記録する
                _goldenScrewStatusArray[_currentWaveIndex] = _getGoldenScrewWithoutClear;
                return _getGoldenScrewWithoutClear;
            }

            return null;
        }


        /// <summary>
        /// 「金のネジ」の最終的な取得情報を取得
        /// Finishシーケンスで呼ばれる想定
        /// </summary>
        /// <returns>最終的な「金のネジ」取得情報</returns>
        public bool?[] GetStatus()
        {
            return _goldenScrewStatusArray;
        }


        void IDisposable.Dispose()
        {
            //購読解除用
            _disposables.Dispose();
        }
    }
}