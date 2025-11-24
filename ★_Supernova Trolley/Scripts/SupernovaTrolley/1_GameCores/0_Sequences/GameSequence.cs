using Cysharp.Threading.Tasks;
using SupernovaTrolley.Audios;
using SupernovaTrolley.Directions;
using SupernovaTrolley.Enemies;
using SupernovaTrolley.GameCores.EndConditions;
using SupernovaTrolley.GameCores.Statuses;
using SupernovaTrolley.Players;
using Utilities;
using VContainer;

namespace SupernovaTrolley.GameCores.Sequences
{
    /// <summary>
    /// ゲームシーケンス
    /// </summary>
    public class GameSequence
    {
        //システム
        [Inject] private readonly SkyboxSystem _skyboxSystem;
        [Inject] private readonly EnemySystem _enemySystem;
        //[Inject] private readonly HitPointStatus _hitPointStatus;
        [Inject] private readonly ScoreStatus _scoreStatus;
        [Inject] private readonly TimeStatus _timeStatus;
        [Inject] private readonly GameFinishableCollection _gameFinishableCollection;
        [Inject] private readonly Player _player;
        [Inject] private readonly BGMManager _bgmManager;

        //ゲーム終了処理用
        private GameResultType _gameResultType = GameResultType.None;
        public GameResultType GameResultType
        {
            get => _gameResultType;
            set
            {
                //一度設定されたら変更しない
                if (_gameResultType == GameResultType.None)
                {
                    _gameResultType = value;
                }
            }
        }


        /// <summary>
        /// ゲームシーケンスを再生する
        /// </summary>
        /// <returns></returns>
        public async UniTask PlaySequence()
        {
            MyLogger.Log($"<color=yellow>ゲームシーケンス再生</color>");
            await PlayStartSequence();
            await UniTask.WaitUntil(() => _gameResultType != GameResultType.None);
            await PlayFinishSequence();
        }


        /// <summary>
        /// ユーザの操作開始までのシーケンスを再生する
        /// </summary>
        /// <returns></returns>
        private async UniTask PlayStartSequence()
        {
            MyLogger.Log($"<color=yellow>ゲームシーケンス：操作開始まで</color>");

            //BGM再生
            _bgmManager.PlayBGM(BGM.NormalBGM);

            //ステータスの初期化
            //_hitPointStatus.InitStatus();
            _scoreStatus.InitStatus();
            _timeStatus.InitStatus();

            //ルールを適用
            _gameFinishableCollection.ApplyFinishRule((GameResultType result) => GameResultType = result);

            //カウントダウン開始
            _timeStatus.StartTimer();

            //スカイボックスシステム開始
            _skyboxSystem.StartSupernova();

            //敵システム開始
            _enemySystem.StartEnemySpawn();

            //プレイヤー操作許可
            _player.StartShooting();

            await UniTask.Yield();
        }


        /// <summary>
        /// ユーザの操作停止からのシーケンスを再生する
        /// </summary>
        /// <returns></returns>
        private async UniTask PlayFinishSequence()
        {
            MyLogger.Log($"<color=yellow>ゲームシーケンス：操作停止から</color>");

            //プレイヤー操作停止
            _player.StopShooting();

            //敵システムを停止
            _enemySystem.StopEnemySpawn();

            //カウントダウン停止
            _timeStatus.ResetTimer();

            //ルールを解除
            _gameFinishableCollection.RemoveFinishRule();

            MyLogger.Log($"<color=cyan>このゲームの結果：{_gameResultType.ToString()}</color>");

            await UniTask.Yield();
        }
    }
}