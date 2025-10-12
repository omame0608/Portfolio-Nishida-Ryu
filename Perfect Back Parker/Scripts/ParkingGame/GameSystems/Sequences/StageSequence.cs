using Cysharp.Threading.Tasks;
using ParkingGame.Audio;
using ParkingGame.Data;
using ParkingGame.Field;
using ParkingGame.GameSystems.Database;
using ParkingGame.GameSystems.EndCondition;
using ParkingGame.GameSystems.View;
using ParkingGame.HUD;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VContainer;

namespace ParkingGame.GameSystems.Sequences
{
    /// <summary>
    /// ステージ開始から終了までを管理するクラス
    /// </summary>
    public class StageSequence
    {
        //参照
        [Inject] private readonly ParkingGameFinishableCollection _finishableCollection;
        [Inject] private readonly IStageDatabaseFacade _stageDatabaseFacade;
        [Inject] private readonly IStageStartView _stageStartView;
        [Inject] private readonly IStageTimerView _stageTimerView;
        [Inject] private readonly IWinResultView _winResultView;
        [Inject] private readonly ILoseResultView _loseResultView;
        [Inject] private readonly IPlayerSystem _playerSystem;
        [Inject] private readonly ICameraSystem _cameraSystem;
        [Inject] private readonly StageTimer _stageTimer;
        [Inject] private readonly IClearCountView _clearCountView;
        [Inject] private readonly IObjectResolver _resolver;

        //シーケンス処理用
        private int _currentStageNumber; //現在進行中のステージ番号
        private GameResultType _resultType = GameResultType.None; //ステージのクリア状況
        public GameResultType ResultType
        {
            get => _resultType;
            set
            {
                if (value == GameResultType.None || _resultType == GameResultType.None)
                {
                    _resultType = value;
                }
            }
        }
        private GameObject _currentStage; //現在のステージプレハブ


        /// <summary>
        /// ステージ開始から終了までのシーケンスを管理
        /// </summary>
        /// <param name="stageNumber">呼び出すステージ番号</param>
        /// <returns>ステージをクリアしたかどうか</returns>
        public async UniTask<bool> PlaySequence(int stageNumber)
        {
            _currentStageNumber = stageNumber;

            //ステージ終了までの流れを管理
            await PlayStartSequence();
            await UniTask.WaitUntil(() => _resultType != GameResultType.None);
            await PlayEndSequence();

            //ステージをクリアしたかどうかを返す
            if (_resultType == GameResultType.Win) return true;
            else return false;
        }


        /// <summary>
        /// プレイヤーの操作が始まるまでのシーケンスを管理
        /// </summary>
        /// <returns></returns>
        private async UniTask PlayStartSequence()
        {
            Debug.Log($"<color=green>ステージ{_currentStageNumber}を開始します</color>");

            //プレイヤーを初期位置にセット
            _playerSystem.InitPlayerPosition();
            _cameraSystem.InitCameraPosition();

            //ステージクリア状況を初期化
            ResultType = GameResultType.None;
            _stageTimerView.HideTimer();
            _clearCountView.Cancel();

            //ステージデータを取得
            var stageData = _stageDatabaseFacade.GetInfoWithStageNumber(_currentStageNumber);

            //ステージ生成
            _currentStage = Object.Instantiate(stageData.StagePrefab);
            _resolver.Inject(_currentStage.GetComponent<StageInitialization>());

            //ステージ情報を表示
            await _stageStartView.ShowStageInfomation(stageData);

            //カウントダウン開始
            _stageTimer.StartStageTimer(stageData.TimeLimit);
            _stageTimerView.ShowTimer();

            //ルール判定を適用
            _finishableCollection.ApplyFinishRule((GameResultType resultType) => ResultType = resultType);
            _clearCountView.CanDisplayCircle = true;

            //操作を可能にする
            _playerSystem.CanControll = true;
            _cameraSystem.CanControll = true;
        }


        /// <summary>
        /// ステージ終了が判定されてからのシーケンスを管理
        /// </summary>
        /// <returns></returns>
        private async UniTask PlayEndSequence()
        {
            //カウント停止
            _stageTimer.StopStageTimer();
            _clearCountView.Cancel();
            _clearCountView.CanDisplayCircle = false;

            //操作を不可能にする
            _playerSystem.CanControll = false;
            _cameraSystem.CanControll = false;

            //ステージクリアだった場合の処理
            if (_resultType == GameResultType.Win)
            {
                Debug.Log($"ステージクリア:Enterキーで次ステージへ");
                _winResultView.ShowWinResult();
                await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                _winResultView.HideWinResult(_currentStageNumber == 10);
            }
            //ステージ失敗だった場合の処理
            else if (_resultType == GameResultType.Lose)
            {
                Debug.Log($"ステージ失敗:Enterキーでリプレイ");
                _loseResultView.ShowLoseResult();
                await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                _loseResultView.HideLoseResult();
            }

            //ステージを削除
            Object.Destroy(_currentStage);

            //エフェクトを削除
            _playerSystem.DestroySmokeEffect();

            Debug.Log($"<color=red>ステージ{_currentStageNumber}を終了します</color>");
        }
    }
}
