using Cysharp.Threading.Tasks;
using PerfectBackParkerRev.Databases;
using PerfectBackParkerRev.GameCores.GameSystems.Sequences;
using PerfectBackParkerRev.GameCores.GameSystems.Views;
using PerfectBackParkerRev.GameCores.Repositories;
using PerfectBackParkerRev.Utilities;
using System.Threading.Tasks;
using VContainer;

namespace PerfectBackParkerRev.GameCores.GameSystems
{
    /// <summary>
    /// ゲームシステム
    /// </summary>
    public class GameSystem : IGameInputHandler
    {
        //システム
        [Inject] private readonly SceneLoader _sceneLoader;
        [Inject] private readonly StageStartSequence _stageStartSequence;
        [Inject] private readonly WaveSequence _waveSequence;
        [Inject] private readonly StageFinishSequence _stageFinishSequence;
        [Inject] private readonly IStageDatabaseFacade _stageDatabaseFacade;
        [Inject] private readonly IPauseView _pauseView;

        //シーケンス制御用
        private int _stageNumber; //管理するステージ番号
        private StageData _stageData; //管理するステージのデータ
        private int _totalWaveNumber; //管理するステージの総ウェーブ数
        private int _currentWaveNumber = 1; //現在のウェーブ番号
        private bool _isStageClear; //ステージを最終ウェーブまでクリアしたかどうか


        /// <summary>
        /// シーンロードを待機した後ゲームシステムを起動する
        /// </summary>
        public async Task Initialize()
        {
            //シーンロード完了を待機
            await UniTask.WaitUntil(() => !_sceneLoader.IsLoading);

            //ゲームシステムを起動
            StartSequence();
        }


        /// <summary>
        /// ゲーム進行シーケンスを適切な順で再生する
        /// </summary>
        private async void StartSequence()
        {
            //管理するステージ番号を取得
            SceneType stage = _sceneLoader.GetCurrentSceneType();
            _stageNumber = stage switch
            {
                SceneType.Stage1 => 1,
                SceneType.Stage2 => 2,
                SceneType.Stage3 => 3,
                SceneType.Stage4 => 4,
                SceneType.Stage5 => 5,
                _ => -1
            };
            if (_stageNumber == -1) MyLogger.LogError($"未対応のシーンです: {stage}");

            //管理するステージのデータを取得
            _stageData = _stageDatabaseFacade.GetInfoWithStageNumber(_stageNumber);

            //管理するステージのウェーブ数を取得
            _totalWaveNumber = _stageData.WaveDataList.Count;

            //テスト用ログ
            MyLogger.Log($"<color=cyan>[GameSystem]現在のステージ番号 : STAGE{_stageNumber}, 総WAVE数 : {_totalWaveNumber}</color>");

            //ステージ開始のシーケンスを再生
            await _stageStartSequence.PlaySequence(_stageData);

            while (!_isStageClear)
            {
                //呼び出すウェーブのデータを取得
                var waveData = _stageData.WaveDataList[_currentWaveNumber - 1];

                //ウェーブシーケンスを再生
                var isWaveClear = await _waveSequence.PlaySequence(waveData);

                //ウェーブをクリアしている場合次のウェーブへ
                if (isWaveClear) _currentWaveNumber++;

                //最終ウェーブをクリアしていたらステージを終了する
                if (_currentWaveNumber > _totalWaveNumber) _isStageClear = true;
            }

            //ステージ終了のシーケンスを再生
            await _stageFinishSequence.PlaySequence(_stageNumber);
        }


        public void OnInputEvent(GameSceneInputType inputType)
        {
            //入力タイプ別の処理
            switch (inputType)
            {
                //ポーズパネルを開く処理
                case GameSceneInputType.Pause:
                    _pauseView.ShowPauseView();
                    break;
                case GameSceneInputType.Return:
                    _stageStartSequence.NotifyReturnInput();
                    _waveSequence.NotifyReturnInput();
                    _stageFinishSequence.NotifyReturnInput();
                    break;
                //エラーハンドリング
                default:
                    MyLogger.LogError($"未対応の入力タイプです: {inputType}");
                    break;
            }
        }
    }
}