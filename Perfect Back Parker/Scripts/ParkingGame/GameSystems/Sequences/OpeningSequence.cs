using Cysharp.Threading.Tasks;
using DG.Tweening;
using ParkingGame.GameSystems.View;
using ParkingGame.HUD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace ParkingGame.GameSystems.Sequences
{
    /// <summary>
    /// オープニングからステージ開始までを管理するクラス
    /// </summary>
    public class OpeningSequence
    {
        //View
        [Inject] private readonly IOpeningView _openingView;

        /// <summary>
        /// オープングからステージ開始までのシーケンスを管理
        /// </summary>
        public async UniTask PlaySequence()
        {
            //オープニング終了までの流れを管理
            _openingView.PlayOpeningView();
            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            await _openingView.FinishOpeningView();
        }
    }
}