using Alchemy.Inspector;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.GameCores.StageSelectSystems.Views;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace PerfectBackParkerRev.StageSelectHUDs
{
    /// <summary>
    /// ステージ開始のトランジション
    /// </summary>
    public class StageStartTransition : MonoBehaviour, IStageStartTransition
    {
        //システム
        [Inject] private readonly SceneLoader _sceneLoader;

        //Audio
        [Inject] private readonly SEManager _seManager;

        [Header("操作対象のオブジェクト")]
        [SerializeField] private Image _fadeImage; //フェード用イメージ
        [SerializeField] private RectTransform _fadeTr; //フェード用

        //移動先シーン
        private SceneType _nextSceneType; //遷移先のシーンタイプ

        //キャンセル用
        private Sequence _transitionSequence; //トランジション用のSequence


        public void AwakeTransition(SceneType nextSceneType)
        {
            _nextSceneType = nextSceneType;
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
            _seManager.PlaySE(SE.StageEnter);

            //シーン遷移シーケンス:シーンロード前
            _transitionSequence = DOTween.Sequence()
                //フェードアウト
                .Append(DOTween.To(() => fadeMat.GetFloat("_Transition"),
                                    x => fadeMat.SetFloat("_Transition", x),
                                    0.98f, 0.5f)
                                .SetEase(Ease.OutQuint))
                .AppendInterval(0.2f)
                .Append(DOTween.To(() => fadeMat.GetFloat("_Transition"),
                                    x => fadeMat.SetFloat("_Transition", x),
                                    0f, 0.8f)
                                .SetEase(Ease.InQuint))
                .Join(_fadeTr.DORotate(new Vector3(0f, 0f, 360f), 0.8f)
                                .SetRelative()
                                .SetEase(Ease.InQuint))
                //シーンを遷移
                .AppendInterval(0.1f)
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                 {
                     _transitionSequence = null;
                 });
            await _transitionSequence.AsyncWaitForCompletion().AsUniTask();

            //シーン遷移
            await _sceneLoader.LoadScene(SceneType.StageSelectScene, _nextSceneType);

            //シーン遷移シーケンス:シーンロード後
            _transitionSequence = DOTween.Sequence()
                .AppendInterval(0.1f)
                //フェードイン
                .Append(DOTween.To(() => fadeMat.GetFloat("_Transition"),
                                    x => fadeMat.SetFloat("_Transition", x),
                                    1f, 0.5f))
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
        [Button] private void TestAwakeTransition()=> AwakeTransition(SceneType.Stage1);
    }
}