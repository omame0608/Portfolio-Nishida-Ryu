using Cysharp.Threading.Tasks;
using ParkingGame.Audio;
using ParkingGame.GameSystems.Sequences;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace ParkingGame.GameSystems
{
    /// <summary>
    /// ゲームのロジックドメインを管理するクラス
    /// </summary>
    public class GameSystem
    {
        //Audio
        [Inject] private readonly BGMManager _bgmManager;

        //参照
        [Inject] private readonly StageSequence _stageSequence;
        [Inject] private readonly OpeningSequence _openingSequence;
        [Inject] private readonly EndingSequence _endingSequence;

        //シーケンス処理用
        private int _currentStageNum = 1; //現在進行しているステージ番号
        private bool _isGameClear; //全ステージが終了したかどうか

        //定数
        private const int _TOTAL_STAGE_COUNT = 10; //総ステージ数


        /// <summary>
        /// 初期化メソッド
        /// </summary>
        public async void Initialize()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));

            //ゲームシーケンスを開始する
            StartSequence();
        }


        /// <summary>
        /// 各種シーケンスを順に呼び出し待機
        /// </summary>
        /// <returns></returns>
        private async void StartSequence()
        {
            _bgmManager.PlayBGM(BGM.Environment);

            //オープニングシーケンスを呼び出す
            await _openingSequence.PlaySequence();

            _bgmManager.PlayBGM(BGM.NormalBGM);

            while (!_isGameClear)
            {
                //ステージシーケンスを呼び出す
                bool isStageClear = await _stageSequence.PlaySequence(_currentStageNum);

                //ステージをクリアしていれば次のコースを指定する
                if (isStageClear) _currentStageNum++;

                //全てのステージをクリアしていればゲームを終了する
                if (_currentStageNum > _TOTAL_STAGE_COUNT) _isGameClear = true;
            }

            //エンディングシーケンスを呼び出す
            await _endingSequence.PlaySequence();

            Debug.Log($"<color=red>全ゲームシーケンスを終了します</color>");
        }
    }
}
