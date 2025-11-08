using Cysharp.Threading.Tasks;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.GameCores.Repositories;
using PerfectBackParkerRev.GameCores.StageSelectSystems.Sequences;
using PerfectBackParkerRev.GameCores.StageSelectSystems.Views;
using PerfectBackParkerRev.Utilities;
using UnityEngine;
using VContainer;

namespace PerfectBackParkerRev.GameCores.StageSelectSystems
{
    /// <summary>
    /// ステージセレクトシステム
    /// </summary>
    public class StageSelectSystem : IStageSelectInputHandler
    {
        //システム
        [Inject] private readonly StageSelectInSceneSequence _stageSelectInSceneSequence;
        [Inject] private readonly StageSelectMoveSequence _stageSelectMoveSequence;
        [Inject] private readonly EndingSequence _endingSequence;
        [Inject] private readonly SceneLoader _sceneLoader;
        [Inject] private readonly IStageStartTransition _stageStartTransition;
        [Inject] private readonly ISaveSystemFacade _saveSystemFacade;

        //Audio
        [Inject] private readonly SEManager _seManager;
        [Inject] private readonly BGMManager _bgmManager;

        //制御用
        private bool _canInteract = false; //入力に対して応答が可能か
        private const int _TOTAL_STAGES = 3; //総ステージ数
        private int _currentLatestStageNumber; //現在遊べる最新のステージ番号
        private int _currentStageNumber = 1; //現在選択中のステージ番号


        /// <summary>
        /// シーンロードを待機した後ステージセレクトシステムを起動する
        /// </summary>
        public async void Initialize()
        {
            //シーンロード完了を待機
            await UniTask.WaitUntil(() => !_sceneLoader.IsLoading);

            //ゲームシステムを起動
            StartSequence();
        }

        
        /// <summary>
        /// ステージセレクトシーケンスを再生する
        /// </summary>
        private async void StartSequence()
        {
            //どのシーンから遷移してきたか取得
            var previousScene = _sceneLoader.GetPreviousSceneType();

            //遷移前のシーンに応じた処理
            switch (previousScene)
            {
                case SceneType.OpeningScene: _currentStageNumber = 1; break;
                case SceneType.Stage1: _currentStageNumber = 1; break;
                case SceneType.Stage2: _currentStageNumber = 2; break;
                case SceneType.Stage3: _currentStageNumber = 3; break;
                case SceneType.Stage4: _currentStageNumber = 4; break;
                case SceneType.Stage5: _currentStageNumber = 5; break;
                default: Debug.LogError($"未対応の遷移元シーンです: {previousScene}"); break;
            }

            //現在遊べる最新のステージ番号を更新
            _currentLatestStageNumber = 1;
            for (int i = 1; i <= _TOTAL_STAGES; i++)
            {
                var score = _saveSystemFacade.LoadStageHighScore((SaveKey)i);
                if (score > 0)
                {
                    _currentLatestStageNumber++;
                }
                else
                {
                    break;
                }
            }
            MyLogger.Log($"<color=green>現在遊べる最新のステージ:{_currentLatestStageNumber}</color>");

            //インシーンのシーケンスを再生
            await _stageSelectInSceneSequence.PlaySequence(_currentStageNumber, _currentLatestStageNumber);
            _canInteract = true;
        }


        public async void OnInputEvent(StageSelectSceneInputType inputType)
        {
            //入力応答が不可能なら処理を終わる
            if (!_canInteract && inputType != StageSelectSceneInputType.BackGame) return;

            //入力タイプ別の処理
            switch (inputType)
            {
                //次のステージを選択する処理
                case StageSelectSceneInputType.MoveNextStage:

                    //遊べる最新のステージを超える場合は処理を終わる
                    if (_currentStageNumber >= _currentLatestStageNumber)
                    {
                        _seManager.PlaySE(SE.StageStopArrow);
                        return;
                    }

                    //SE再生
                    _seManager.PlaySE(SE.StageSelectArrow);

                    //現在選択中のステージ番号を更新
                    _currentStageNumber++;
                    if (_currentStageNumber > _TOTAL_STAGES)
                    {
                        //エンディング枠に移動する場合エンディングシーケンスへ移行
                        _canInteract = false;
                        await _endingSequence.PlaySequence();
                        _canInteract = true;
                        _currentStageNumber--;
                        return;
                    }

                    //シーケンスを再生
                    _canInteract = false;
                    await _stageSelectMoveSequence.PlaySequence(true, _currentStageNumber);
                    _canInteract = true;
                    break;

                //前のステージを選択する処理
                case StageSelectSceneInputType.MovePreviousStage:
                    //現在選択中のステージ番号を更新
                    if (_currentStageNumber <= 1)
                    {
                        _seManager.PlaySE(SE.StageStopArrow);
                        return;
                    }
                    _currentStageNumber--;

                    //SE再生
                    _seManager.PlaySE(SE.StageSelectArrow);

                    //シーケンスを再生
                    _canInteract = false;
                    await _stageSelectMoveSequence.PlaySequence(false, _currentStageNumber);
                    _canInteract = true;
                    break;

                //ステージを開始する処理
                case StageSelectSceneInputType.StartStage:
                    if (_currentStageNumber <= _TOTAL_STAGES)
                    {
                        //ステージシーンへ遷移する
                        Debug.Log($"<color=yellow>ステージを開始します：Stage{_currentStageNumber}</color>");
                        _stageStartTransition.AwakeTransition((SceneType)(_currentStageNumber + 1));

                        //BGMフェードアウト
                        _bgmManager.StopBGM(true);
                    }
                    break;
                //ゲームに戻る処理
                case StageSelectSceneInputType.BackGame:
                    _endingSequence.NotifyGameBackInput();
                    break;
                //エラーハンドリング
                default:
                    Debug.LogError($"未対応の入力タイプです: {inputType}");
                    break;
            }
        }
    }
}