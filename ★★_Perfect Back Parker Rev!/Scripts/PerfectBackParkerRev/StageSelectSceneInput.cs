using PerfectBackParkerRev.GameCores.StageSelectSystems;
using PerfectBackParkerRev.Utilities;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using VContainer;

namespace PerfectBackParkerRev
{
    /// <summary>
    /// ステージセレクト画面の入力受付
    /// </summary>
    public class StageSelectSceneInput : MonoBehaviour
    {
        //ハンドラ
        [Inject] private IStageSelectInputHandler _stageSelectInputHandler;

        //UI要素
        [SerializeField] private ClickableButton _nextStageButton; //次ステージボタン
        [SerializeField] private ClickableButton _previousStageButton; //前ステージボタン
        [SerializeField] private ClickableButton _startStageButton; //ステージ開始ボタン


        private void Start()
        {
            //----------次ステージへ----------
            //UIボタン操作
            _nextStageButton.OnClickSubject.Subscribe(_ =>
            {
                _stageSelectInputHandler.OnInputEvent(StageSelectSceneInputType.MoveNextStage);
            })
            .AddTo(this);
            //キー操作
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.LeftArrow)
                            || Input.GetKeyDown(KeyCode.A)
                            || Input.GetKeyDown(KeyCode.S))
                .Subscribe(_ =>
                {
                    _stageSelectInputHandler.OnInputEvent(StageSelectSceneInputType.MoveNextStage);
                })
                .AddTo(this);

            //----------前ステージへ----------
            //UIボタン操作
            _previousStageButton.OnClickSubject.Subscribe(_ =>
            {
                _stageSelectInputHandler.OnInputEvent(StageSelectSceneInputType.MovePreviousStage);
            })
            .AddTo(this);
            //キー操作
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.RightArrow)
                            || Input.GetKeyDown(KeyCode.D)
                            || Input.GetKeyDown(KeyCode.W))
                .Subscribe(_ =>
                {
                    _stageSelectInputHandler.OnInputEvent(StageSelectSceneInputType.MovePreviousStage);
                })
                .AddTo(this);

            //----------ステージ開始----------
            //UIボタン操作
            _startStageButton.OnClickSubject.Subscribe(_ =>
            {
                _stageSelectInputHandler.OnInputEvent(StageSelectSceneInputType.StartStage);
            })
            .AddTo(this);
            //キー操作
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.Return))
                .Subscribe(_ =>
                {
                    _stageSelectInputHandler.OnInputEvent(StageSelectSceneInputType.StartStage);
                })
                .AddTo(this);

            //----------ゲームに戻る----------
            //キー操作
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
                .Subscribe(_ =>
                {
                    _stageSelectInputHandler.OnInputEvent(StageSelectSceneInputType.BackGame);
                })
                .AddTo(this);
        }
    }
}