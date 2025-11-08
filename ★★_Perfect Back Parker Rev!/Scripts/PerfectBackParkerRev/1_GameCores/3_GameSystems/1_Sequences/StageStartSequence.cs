using Cysharp.Threading.Tasks;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.Databases;
using PerfectBackParkerRev.GameCores.GameSystems.ItemPickUps;
using PerfectBackParkerRev.GameCores.GameSystems.Views;
using PerfectBackParkerRev.GameCores.Repositories;
using PerfectBackParkerRev.GameCores.Users;
using PerfectBackParkerRev.Utilities;
using System;
using VContainer;

namespace PerfectBackParkerRev.GameCores.GameSystems.Sequences
{
    /// <summary>
    /// ステージ開始のシーケンス
    /// ユーザからの車の操作を受け付けるまで
    /// </summary>
    public class StageStartSequence
    {
        //システム
        [Inject] private readonly IPlayerSystem _playerSystem;
        [Inject] private readonly GoldenScrewStatus _goldenScrewStatus;
        [Inject] private readonly IStageStartView _stageStartView;
        [Inject] private readonly ISaveSystemFacade _saveSystemFacade;
        
        //Audio
        [Inject] private readonly BGMManager _bgmManager;

        //制御用
        private bool _isWaitingOpeningDirection = false; //オープニング演出待機中かどうか
        private bool _stopSequenceFlag = true; //特定のタイミングでtrueになるまでシーケンスを停止するフラグ


        /// <summary>
        /// ステージ開始のシーケンスを再生する
        /// </summary>
        /// <param name="stageData">進行中のステージのデータ</param>
        /// <returns></returns>
        public async UniTask PlaySequence(StageData stageData)
        {
            MyLogger.Log($"<color=yellow>StageStartSequenceを開始します</color>");

            //このステージ管理中に利用する「金のネジ」ステータスを初期化
            _goldenScrewStatus.InitStatus(stageData.WaveDataList.Count);

            //このステージのハイスコアを取得
            var key = Enum.Parse<SaveKey>(stageData.StageName.Replace(" ", ""), ignoreCase:true);
            var highScore = _saveSystemFacade.LoadStageHighScore(key);
            
            //ステージ開始の紹介ビューを表示(スキップ可能)
            _isWaitingOpeningDirection = true;
            await _stageStartView.ShowStageAbstractView(stageData, highScore);
            _isWaitingOpeningDirection = false;

            //BGM再生
            var bgm = Enum.Parse<BGM>(stageData.StageName.Replace(" ", ""), ignoreCase: true);
            _bgmManager.PlayBGM(bgm);

            //プレイヤーに物理演算を適用
            _playerSystem.SwitchRigidbody(true);

            //演出がスキップされた場合、補間アニメーションの再生を待機する
            //await UniTask.WaitUntil(() => _stopSequenceFlag);
        }


        /// <summary>
        /// 演出キャンセル入力を通知する
        /// 演出待機中でなければ無視される
        /// </summary>
        public async void NotifyReturnInput()
        {
            //待機中の時のみそれを停止し、アニメーションを補間する
            if (_isWaitingOpeningDirection)
            {
                _stageStartView.CancelShowStageAbstractView();

                //アニメーション補間中はシーケンスを停止
                _stopSequenceFlag = false;
                await _stageStartView.ShowStageStartText();
                _stopSequenceFlag = true;
            }
        }
    }
}