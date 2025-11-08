using Cysharp.Threading.Tasks;
using PerfectBackParkerRev.Databases;
using PerfectBackParkerRev.GameCores.GameSystems.EndConditions;
using PerfectBackParkerRev.GameCores.GameSystems.ItemPickUps;
using PerfectBackParkerRev.GameCores.GameSystems.Scores;
using PerfectBackParkerRev.GameCores.GameSystems.Views;
using PerfectBackParkerRev.GameCores.Users;
using PerfectBackParkerRev.Utilities;
using System;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace PerfectBackParkerRev.GameCores.GameSystems.Sequences
{
    /// <summary>
    /// ステージ中の1ウェーブ分のシーケンス
    /// </summary>
    public class WaveSequence
    {
        //システム
        [Inject] private readonly IPlayerSystem _playerSystem;
        [Inject] private readonly IPlayerCameraSystem _cameraSystem;
        [Inject] private readonly WaveFinishableCollection _waveFinishableCollection;
        [Inject] private readonly WaveTimer _waveTimer;
        [Inject] private readonly GoldenScrewStatus _goldenScrewStatus;
        [Inject] private readonly ScoreStatus _scoreStatus;
        [Inject] private readonly IWaveStartView _waveStartView;
        [Inject] private readonly IWaveTimerView _waveTimerView;
        [Inject] private readonly IClearCountView _clearCountView;
        [Inject] private readonly IFailedView _failedView;
        [Inject] private readonly IClearView _clearView;
        [Inject] private readonly IObjectResolver _resolver;

        //シーケンス制御用
        private WaveData _currentWaveData; //現在進行管理中のウェーブデータ
        private GameObject _currentWaveGameObject; //現在配置中のウェーブプレハブ
        private const float _RETURN_DELAY_SECONDS = 0.5f; //ウェーブ情報表示後の待機時間(秒)
        private const float _FAILED_DISPLAY_SECONDS = 0.5f; //失敗結果表示時間(秒)
        private const float _CLEAR_DISPLAY_SECONDS = 1.5f; //クリア結果表示時間(秒)

        //入力通知用
        private UniTaskCompletionSource<bool> _returnInputReceived;

        //ウェーブ終了処理用
        private WaveResultType _waveResultType = WaveResultType.None; //ウェーブ終了時の結果タイプ
        public WaveResultType WaveResultType
        {
            get => _waveResultType;
            set
            {
                //「ウェーブ結果をNoneへ初期化」「Noneからの変更」の2種の更新のみ許可
                if (value == WaveResultType.None || _waveResultType == WaveResultType.None)
                {
                    _waveResultType = value;
                }
            }
        }
        private WaveResultType _lastWaveResultType = WaveResultType.None; //直前のシーケンスの結果


        /// <summary>
        /// ウェーブ開始のシーケンスを再生する
        /// </summary>
        /// <param name="waveData">呼び出すウェーブのデータ</param>
        /// <returns>ウェーブをクリアしたかどうか</returns>
        public async UniTask<bool> PlaySequence(WaveData waveData)
        {
            //await UniTask.Delay(1000);
            MyLogger.Log($"<color=yellow>WaveSequenceを開始します : {waveData.WaveName}</color>");

            //現在進行管理中のウェーブデータを保持
            _currentWaveData = waveData;

            //ウェーブ終了までの流れを管理
            await PlayWaveStartSequence();
            await UniTask.WaitUntil(() => WaveResultType != WaveResultType.None);
            await PlayWaveFinishSequence();
            
            //結果を「直前のシーケンスの結果」として保存
            _lastWaveResultType = WaveResultType;

            //ウェーブをクリアしたかどうかを返す
            if (WaveResultType == WaveResultType.Clear) return true;
            else return false;
        }


        /// <summary>
        /// プレイヤーの操作が始まるまでのシーケンスを再生
        /// </summary>
        /// <returns></returns>
        private async UniTask PlayWaveStartSequence()
        {
            //ステージクリア状況を初期化
            WaveResultType = WaveResultType.None;
            _waveTimerView.HideWaveTimer();

            //ウェーブプレハブを配置し初期化
            _currentWaveGameObject = Object.Instantiate(_currentWaveData.WavePrefab);
            var initialization = _currentWaveGameObject.GetComponent<WavePrefabInitialization>();
            _resolver.Inject(initialization);
            initialization.Init();

            //前シーケンスの結果がありかつ失敗だった場合
            if (_lastWaveResultType == WaveResultType.Failed)
            {
                //プレイヤー・カメラを現在のウェーブのスタート位置へリセット
                //物理演算を一時的にオフにして位置を強制変更
                _playerSystem.SwitchRigidbody(false);
                _playerSystem.Transform.position = _currentWaveData.StartPosition;
                _playerSystem.Transform.rotation = _currentWaveData.IsStartWithZPlusDirection
                    ? Quaternion.Euler(0f, 0f, 0f)
                    : Quaternion.Euler(0f, 180f, 0f);
                _cameraSystem.ResetCameraTransform(_currentWaveData.IsStartWithZPlusDirection);
                _playerSystem.SwitchRigidbody(true);
            }

            //ウェーブ情報を表示
            _waveStartView.ShowWaveInfomation(_currentWaveData);
            await UniTask.Delay(TimeSpan.FromSeconds(_RETURN_DELAY_SECONDS));

            //制限時間のカウントダウンを開始
            _waveTimer.StartWaveTimer(_currentWaveData.TimeLimit);
            _waveTimerView.ShowWaveTimer();

            //ルールを適用
            _waveFinishableCollection.ApplyFinishRule((WaveResultType result) => WaveResultType = result);
            _clearCountView.CanDisplayCircle = true;

            //「金のネジ」ステータスを適用
            _goldenScrewStatus.StartObserve(_currentWaveData.WaveName);

            //プレイヤーの操作を許可
            _playerSystem.CanControll = true;
            _cameraSystem.CanControll = true;
        }


        /// <summary>
        /// ウェーブ終了が判定されてからのシーケンスを再生
        /// </summary>
        /// <returns></returns>
        private async UniTask PlayWaveFinishSequence()
        {
            //プレイヤーの操作を禁止
            _playerSystem.CanControll = false;
            _cameraSystem.CanControll = false;

            //制限時間のカウントを終了
            _waveTimer.ResetWaveTimer();

            //ルールを解除
            _waveFinishableCollection.RemoveFinishRule();
            _clearCountView.CanDisplayCircle = false;

            //「金のネジ」ステータスを解除
            var waveScrewResult = _goldenScrewStatus.FinishObserve(WaveResultType == WaveResultType.Clear);

            //ウェーブクリアだった場合の処理
            if (WaveResultType == WaveResultType.Clear)
            {
                //ウェーブのスコアを計算
                var scores = ScoreCalculator.CalculateWaveScore(
                    waveBaseScore: _currentWaveData.WaveBaseScore,
                    timeLimit: _currentWaveData.TimeLimit,
                    remainingTime: _waveTimer.RemainingTime,
                    isNoMissClear: _lastWaveResultType != WaveResultType.Failed,
                    waveScrewResult: waveScrewResult ?? false
                );

                //ウェーブスコアを保持する
                _scoreStatus.SetWaveScore(scores);

                //クリアViewを表示する
                _clearView.ShowClearResult(scores);

                //一定時間待機
                await UniTask.Delay(TimeSpan.FromSeconds(_CLEAR_DISPLAY_SECONDS));

                //リターン入力を待機
                _returnInputReceived = new UniTaskCompletionSource<bool>();
                await _returnInputReceived.Task;
                _returnInputReceived = null;

                //クリアViewを非表示にする
                _clearView.HideClearResult(_currentWaveData.WaveName == "WAVE 8");
            }
            //ウェーブ失敗だった場合の処理    
            else if (WaveResultType == WaveResultType.Failed)
            {
                //当たり判定を失敗時状態に変更
                _playerSystem.SwitchCollision(false);

                //失敗Viewを表示する
                _failedView.ShowFailedResult();

                //一定時間待機
                await UniTask.Delay(TimeSpan.FromSeconds(_FAILED_DISPLAY_SECONDS));

                //リターン入力を待機
                _returnInputReceived = new UniTaskCompletionSource<bool>();
                await _returnInputReceived.Task;
                _returnInputReceived = null;

                //失敗Viewを非表示にする
                _failedView.HideFailedResult();

                //当たり判定を通常時状態に変更
                _playerSystem.SwitchCollision(true);
            }

            //ウェーブプレハブを削除
            Object.Destroy(_currentWaveGameObject);
        }


        /// <summary>
        /// Enterキーまたはクリックの入力があったことを通知する
        /// 入力待機中でなければ無視される
        /// </summary>
        public void NotifyReturnInput()
        {
            //リターンの入力があったことを通知
            _returnInputReceived?.TrySetResult(true);
        }
    }
}