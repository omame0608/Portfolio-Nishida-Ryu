using Cysharp.Threading.Tasks;
using SupernovaTrolley.Audios;
using SupernovaTrolley.Directions;
using SupernovaTrolley.HUDs;
using System;
using Utilities;
using VContainer;

namespace SupernovaTrolley.GameCores.Sequences
{
    /// <summary>
    /// オープニングシーケンス
    /// </summary>
    public class OpeningSequence
    {
        //システム
        [Inject] private readonly StatusView _statusView;
        [Inject] private readonly RailScrollSystem _railScrollSystem;
        [Inject] private readonly BGMManager _bgmManager;

        private bool _gameStartFlag = false;


        /// <summary>
        /// オープニングシーケンスを再生する
        /// </summary>
        /// <returns></returns>
        public async UniTask PlaySequence()
        {
            MyLogger.Log($"<color=yellow>オープニングシーケンス再生</color>");
            //BGM再生
            _bgmManager.PlayBGM(BGM.Environment);

            //ゲーム開始フラグが立つまで待機
            await UniTask.WaitUntil(() => _gameStartFlag);

            //レールスクロール開始
            _railScrollSystem.UseRailScrollSystem();

            //一定時間待機
            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            //ステータスビューを表示
            _statusView.ShowStatusView();

            //一定時間待機してからシーケンスを終了する
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
        }


        /// <summary>
        /// ゲーム開始フラグを立てる
        /// </summary>
        public void SetGameStartFlag()
        {
            _gameStartFlag = true;
        }
    }
}