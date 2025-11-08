using Alchemy.Inspector;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.GameCores.GameSystems.Views;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace PerfectBackParkerRev.GameHUDs
{
    /// <summary>
    /// ステージ終了のトランジション
    /// </summary>
    public class StageFinishTransition : MonoBehaviour, IStageFinishTransition
    {
        //システム
        [Inject] private readonly SceneLoader _sceneLoader;

        //Audio
        [Inject] private readonly SEManager _seManager;

        [Header("操作対象のオブジェクト")]
        [SerializeField] private Image _fadeImage; //フェード用イメージ

        //移動元シーン
        private SceneType _currentSceneType; //現在のシーンタイプ

        //キャンセル用
        private Sequence _transitionSequence; //トランジション用のSequence

        //操作用
        private bool _isReplay = false;


        public void AwakeTransition(SceneType currentSceneType, bool isReplay = false)
        {
            _isReplay = isReplay;
            _currentSceneType = currentSceneType;
            gameObject.SetActive(true);
        }


        public async void Start()
        {
            //シーンを跨いでも破棄されないようにする
            DontDestroyOnLoad(gameObject);

            //トランジションが残っていたらキル
            _transitionSequence?.Kill(true);

            //初期設定
            var fadeMat = _fadeImage.material;
            fadeMat.SetFloat("_Transition", 1f);

            //SE再生
            _seManager.PlaySE(SE.TransitionIn);

            //シーン遷移シーケンス：シーンロード前
            _transitionSequence = DOTween.Sequence()
                //フェードアウト
                .Append(DOTween.To(() => fadeMat.GetFloat("_Transition"),
                                    x => fadeMat.SetFloat("_Transition", x),
                                    0f, 1f)
                               .SetEase(Ease.InQuint))
                //シーンを遷移
                .AppendInterval(0.1f)
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _transitionSequence = null;
                });
            await _transitionSequence.AsyncWaitForCompletion().AsUniTask();

            //シーンをロード
            if (_isReplay)
            {
                await _sceneLoader.LoadScene(SceneType.StageSelectScene, _currentSceneType);
            }
            else
            {
                await _sceneLoader.LoadScene(_currentSceneType, SceneType.StageSelectScene);
            }

            //SE再生
            if (!_isReplay)
            {
                _seManager.PlaySE(SE.TransitionOut);
            }

            //シーン遷移シーケンス：シーンロード後
            _transitionSequence = DOTween.Sequence()
                .AppendInterval(0.1f)
                //フェードイン
                .Append(DOTween.To(() => fadeMat.GetFloat("_Transition"),
                                    x => fadeMat.SetFloat("_Transition", x),
                                    1f, 1f)
                               .SetEase(Ease.OutQuint))
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _transitionSequence = null;
                });
            await _transitionSequence.AsyncWaitForCompletion().AsUniTask();

            //自身を破棄
            Destroy(gameObject);
        }


        [Title("テスト用")]
        [Button] private void TestAwakeTransition() => AwakeTransition(_sceneLoader.GetCurrentSceneType());
    }
}