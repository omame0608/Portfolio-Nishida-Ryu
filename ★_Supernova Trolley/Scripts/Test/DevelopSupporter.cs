using Alchemy.Inspector;
using SupernovaTrolley.GameCores.Sequences;
using SupernovaTrolley.GameCores.Statuses;
using UnityEngine;
using VContainer;

namespace Test
{
    /// <summary>
    /// 開発支援用コンポーネント
    /// </summary>
    public class DevelopSupporter : MonoBehaviour
    {
        //システム
        [Inject] private readonly OpeningSequence _openingSequence;
        [Inject] private readonly EndingSequence _endingSequence;
        //[Inject] private readonly HitPointStatus _hitPointStatus;
        [Inject] private readonly ScoreStatus _scoreStatus;


        [Title("ゲームを開始する")]
        [Button]
        public void SetGameStartFlag() => _openingSequence.SetGameStartFlag();

        /*
        [Button]
        public void DamageHitPoint() => _hitPointStatus.DecreaseHP();
        */

        [Title("スコア加点")]
        [Button]
        public void AddScore() => _scoreStatus.AddScore(40);

        [Title("スコア減点")]
        [Button]
        public void SubtractScore() => _scoreStatus.SubtractScore(100);

        [Title("ゲームを終了する")]
        [Button]
        public void SetGameEndFlag() => _endingSequence.SetGameEndFlag();
    }
}
