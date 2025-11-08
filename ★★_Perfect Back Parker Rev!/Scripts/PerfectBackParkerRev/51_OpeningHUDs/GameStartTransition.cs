using DG.Tweening;
using DG.Tweening.Core.Easing;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.GameCores.OpeningSystems.Views;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace PerfectBackParkerRev.OpeningHUDs
{
    /// <summary>
    /// ゲームスタートのトランジション
    /// </summary>
    public class GameStartTransition : MonoBehaviour, IGameStartTransition
    {
        //システム
        [Inject] private readonly SceneLoader _sceneLoader;

        //Audio
        [Inject] private readonly SEManager _seManager;

        [Header("操作対象のオブジェクト")]
        [SerializeField] private Image _fadeImage; //フェード用イメージ

        [Header("テスト用：どのシーンからの遷移だと仮定するか")]
        [SerializeField] private SceneType _fromSceneType = SceneType.OpeningScene;

        //キャンセル用
        private Sequence _transitionSequence; //トランジション用のSequence


        public void AwakeTransition()
        {
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
            fadeMat.SetFloat("_Transition", 0f);

            //シーン遷移シーケンス：シーンロード前
            _transitionSequence = DOTween.Sequence()
                //シーンを遷移
                .AppendInterval(0.1f)
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _transitionSequence = null;
                });
            await _transitionSequence.AsyncWaitForCompletion();

            //シーンをロード
            await _sceneLoader.LoadScene(_fromSceneType, SceneType.StageSelectScene);

            //SE再生
            _seManager.StopAllSE();
            _seManager.PlaySE(SE.TransitionOut);

            //シーン遷移シーケンス：シーンロード後
            _transitionSequence = DOTween.Sequence()
                //フェードイン
                .Append(DOTween.To(() => fadeMat.GetFloat("_Transition"),
                                    x => fadeMat.SetFloat("_Transition", x),
                                    1f, 1f))
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _transitionSequence = null;
                });
            await _transitionSequence.AsyncWaitForCompletion();

            //自身を破棄
            Destroy(gameObject);
        }
    }
}
