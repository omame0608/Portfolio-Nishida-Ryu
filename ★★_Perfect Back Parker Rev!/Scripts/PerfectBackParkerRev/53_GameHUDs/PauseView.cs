using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.GameCores.GameSystems.Views;
using PerfectBackParkerRev.Utilities;
using UniRx;
using UnityEngine;
using VContainer;

namespace PerfectBackParkerRev.GameHUDs
{
    /// <summary>
    /// ポーズビュー
    /// </summary>
    public class PauseView : MonoBehaviour, IPauseView
    {
        //ボタン
        [SerializeField] private ClickableButton _backButton;
        [SerializeField] private ClickableButton _restartButton;
        [SerializeField] private ClickableButton _jumpSelectButton;

        //システム
        [Inject] private readonly IStageFinishTransition _stageFinishTransition;
        [Inject] private readonly SceneLoader _sceneLoader;

        //Audio
        [Inject] private readonly BGMManager _bgmManager;
        [Inject] private readonly SEManager _seManager;


        private void Start()
        {
            //ゲームにもどる
            _backButton.OnClickSubject.Subscribe(_ =>
            {
                Time.timeScale = 1;
                _bgmManager.UnPauseBGM();
                gameObject.SetActive(false);
            }).AddTo(this);

            //リスタート
            _restartButton.OnClickSubject.Subscribe(_ =>
            {
                Time.timeScale = 1;
                _bgmManager.StopBGM(true);
                _stageFinishTransition.AwakeTransition(_sceneLoader.GetCurrentSceneType(), true);
            }).AddTo(this);

            //ステージセレクトへジャンプ
            _jumpSelectButton.OnClickSubject.Subscribe(_ =>
            {
                Time.timeScale = 1;
                _stageFinishTransition.AwakeTransition(_sceneLoader.GetCurrentSceneType());
            }).AddTo(this);
        }


        public void ShowPauseView()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
            _bgmManager.PauseBGM();
        }
    }
}