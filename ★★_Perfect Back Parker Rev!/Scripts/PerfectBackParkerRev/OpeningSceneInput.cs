using PerfectBackParkerRev.GameCores.OpeningSystems;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using VContainer;

namespace PerfectBackParkerRev
{
    /// <summary>
    /// オープニング画面の入力受付
    /// </summary>
    public class OpeningSceneInput : MonoBehaviour
    {
        //ハンドラ
        [Inject] private IOpeningInputHandler _openingInputHandler;


        private void Start()
        {
            //----------ゲームスタート----------
            //キー操作
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.Return)
                            || Input.GetMouseButtonDown(0))
                .Subscribe(_ =>
                {
                    _openingInputHandler.OnInputEvent(OpeningSceneInputType.StartGame);
                })
                .AddTo(this);
        }
    }
}