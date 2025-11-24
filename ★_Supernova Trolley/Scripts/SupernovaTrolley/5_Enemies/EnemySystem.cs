using Alchemy.Inspector;
using DG.Tweening;
using UnityEngine;
using Utilities;
using VContainer;

namespace SupernovaTrolley.Enemies
{
    /// <summary>
    /// 敵のシステム全般を管理するクラス
    /// </summary>
    public class EnemySystem : MonoBehaviour
    {
        //システム
        [Inject] private readonly IObjectResolver _resolver;

        [Title("生成する敵に関するパラメータ")]
        [Header("敵1")]
        [SerializeField] private GameObject _followEnemyPrefab1;
        [SerializeField] private int _weight1;
        [Header("敵2")]
        [SerializeField] private GameObject _followEnemyPrefab2;
        [SerializeField] private int _weight2;

        [Title("各種パラメータ調整")]
        [SerializeField] private float _spawnDistance; // プレイヤーからの生成距離
        [SerializeField] private float _spawnDeltaHeight; // 生成高さの加算分
        [SerializeField] private float _minSpawnRadius; // 最小生成半径
        [SerializeField] private float _MaxSpawnRadius; // 最大生成半径
        [SerializeField] private float _minAngle; // 最小生成角度
        [SerializeField] private float _maxAngle; // 最大生成角度
        [SerializeField] private float _minSpawnInterval; // 最小生成間隔
        [SerializeField] private float _maxSpawnInterval; // 最大生成間隔

        //キャンセル用
        private Sequence _sequence;


        /// <summary>
        /// 敵のランダム生成を開始する
        /// </summary>
        public void StartEnemySpawn()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence()
                .AppendCallback(() =>
                {
                    //生成位置をランダムに決定
                    var angle = Random.Range(_minAngle, _maxAngle) * Mathf.Deg2Rad;
                    var radius = Random.Range(_minSpawnRadius, _MaxSpawnRadius);
                    var spawnPosition = new Vector3(Mathf.Cos(angle)*radius, Mathf.Sin(angle)*radius + _spawnDeltaHeight, _spawnDistance);
                    //敵をランダムに選択
                    var totalWeight = _weight1 + _weight2;
                    var randomValue = Random.Range(0, totalWeight);
                    GameObject enemy = null;
                    if (randomValue < _weight1)
                    {
                        enemy = _followEnemyPrefab1;
                    }
                    else if (randomValue < _weight1 + _weight2)
                    {
                        enemy = _followEnemyPrefab2;
                    }
                    //敵を生成
                    var obj = Instantiate(enemy, spawnPosition, Quaternion.identity);
                    _resolver.Inject(obj.GetComponent<FollowEnemy>());
                })
                .AppendInterval(Random.Range(_minSpawnInterval, _maxSpawnInterval))
                .SetLoops(-1)
                .SetLink(gameObject);
        }


        /// <summary>
        /// 敵のランダム生成を停止する
        /// </summary>
        public void StopEnemySpawn()
        {
            _sequence?.Kill();
            RemoveAllEnemy();
        }


        /// <summary>
        /// シーン内の全ての敵を破棄する
        /// </summary>
        private void RemoveAllEnemy()
        {
            //Enemyタグをすべて破棄
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                Destroy(enemy);
            }
        }


        [Title("デバッグ用")]
        [Button]
        private void TestStartEnemySpawn()
        {
            if (!Application.isPlaying) return;
            StartEnemySpawn();
        }
        [Button]
        private void TestStopEnemySpawn()
        {
            if (!Application.isPlaying) return;
            StopEnemySpawn();
        }
    }
}