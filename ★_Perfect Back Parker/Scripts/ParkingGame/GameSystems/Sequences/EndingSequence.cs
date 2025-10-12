using Cysharp.Threading.Tasks;
using ParkingGame.GameSystems.View;
using ParkingGame.HUD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace ParkingGame.GameSystems.Sequences
{
    /// <summary>
    /// ステージ終了からエンディングまでを管理するクラス
    /// </summary>
    public class EndingSequence
    {
        //参照
        [Inject] private readonly IStageTimerView _stageTimerView;

        //View
        [Inject] private readonly IEndingView _endingView;


        /// <summary>
        /// ステージ終了からエンディングまでのシーケンスを管理
        /// </summary>
        public async UniTask PlaySequence()
        {
            _stageTimerView.HideTimer();

            //エンディングまでの流れを管理
            _endingView.ShowEndingView();
            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

            //リスタート
            SceneManager.LoadScene("StageScene");
        }
    }
}