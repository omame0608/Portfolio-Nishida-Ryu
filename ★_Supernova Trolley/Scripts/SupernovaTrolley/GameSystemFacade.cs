using SupernovaTrolley.GameCores.Sequences;
using SupernovaTrolley.GameCores.Statuses;
using UnityEngine;
using VContainer;

namespace SupernovaTrolley
{
    /// <summary>
    /// ゲームシステムのファサード
    /// Monoレイヤーからロジックレイヤーへの窓口を提供する
    /// </summary>
    public class GameSystemFacade : MonoBehaviour
    {
        //システム
        [Inject] private readonly OpeningSequence _openingSequence;
        [Inject] private readonly EndingSequence _endingSequence;
        //[Inject] private readonly HitPointStatus _hitPointStatus;
        [Inject] private readonly ScoreStatus _scoreStatus;


        //ゲームを開始する
        public void SetGameStartFlag() => _openingSequence.SetGameStartFlag();

        //ダメージをくらう
        //public void DamageHitPoint() => _hitPointStatus.DecreaseHP();

        //スコア加点
        public void AddScore(int add) => _scoreStatus.AddScore(add);

        //スコア減点
        public void SubtractScore(int sub) => _scoreStatus.SubtractScore(sub);

        //ゲームを終了する
        public void SetGameEndFlag() => _endingSequence.SetGameEndFlag();
    }
}