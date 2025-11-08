using Cysharp.Threading.Tasks;
using PerfectBackParkerRev.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace PerfectBackParkerRev
{
    /// <summary>
    /// シーン間でデータを渡しつつシーン遷移を行う
    /// </summary>
    public class SceneLoader : IInitializable
    {
        //保存データ
        private SceneType _previousSceneType = SceneType.None; //前のシーンタイプ
        private SceneType _currentSceneType; //現在のシーンタイプ

        //ロード中フラグ
        private bool _isLoading = false;
        public bool IsLoading => _isLoading;


        void IInitializable.Initialize()
        {
            //シーンローダーの初期化処理
            //初期シーンタイプを判定し記録
            var sceneName = SceneManager.GetActiveScene().name;
            if (System.Enum.TryParse(sceneName, out SceneType sceneType))
            {
                _currentSceneType = sceneType;
                MyLogger.Log($"[SceneLoader]現在のシーンタイプ: {_currentSceneType}");
            }
            else
            {
                MyLogger.LogError($"[SceneLoader]未対応のシーンです: {sceneName}");
                _currentSceneType = SceneType.None;
            }
        }


        /// <summary>
        /// 現在のシーンを保持しつつシーン遷移を行う
        /// </summary>
        /// <param name="currentScene">現在のシーン</param>
        /// <param name="nextScene">シーン遷移後のシーン</param>
        public async UniTask LoadScene(SceneType currentScene, SceneType nextScene)
        {
            //現在のシーンタイプを保存
            _previousSceneType = currentScene;
            //遷移後のシーンタイプを保存
            _currentSceneType = nextScene;

            //シーン遷移
            MyLogger.Log($"<color=white>[SceneLoader]シーンロード開始</color>");
            _isLoading = true;
            await SceneManager.LoadSceneAsync(nextScene.ToString());
            _isLoading = false;
            MyLogger.Log($"<color=white>[SceneLoader]シーンロード完了</color>");
        }


        /// <summary>
        /// 前のシーンタイプを取得する
        /// </summary>
        /// <returns>前のシーンタイプ</returns>
        public SceneType GetPreviousSceneType()
        {
            return _previousSceneType;
        }


        /// <summary>
        /// 今のシーンタイプを取得する
        /// </summary>
        /// <returns>今のシーンタイプ</returns>
        public SceneType GetCurrentSceneType()
        {
            return _currentSceneType;
        }
    }


    /// <summary>
    /// シーンを識別するための列挙型
    /// </summary>
    public enum SceneType
    {
        OpeningScene,
        StageSelectScene,
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        Stage5,
        None
    }
}