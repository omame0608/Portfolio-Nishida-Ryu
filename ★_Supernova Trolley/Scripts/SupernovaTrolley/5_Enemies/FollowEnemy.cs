using Alchemy.Inspector;
using SupernovaTrolley.Audios;
using UnityEngine;
using Utilities;
using VContainer;

namespace SupernovaTrolley.Enemies
{
    /// <summary>
    /// プレイヤーを追いかける敵
    /// </summary>
    public class FollowEnemy : MonoBehaviour
    {
        //システム
        [Inject] private readonly GameSystemFacade _gameSystemFacade;
        [Inject] private readonly SEManager _seManager;

        //追従対象
        private GameObject player;

        [Title("各種パラメータ調整")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _addScoreValue;
        [SerializeField] private int _subScoreValue;

        [Title("エフェクト")]
        [SerializeField] private GameObject _destroyEffectPrefab;


        private void Start()
        {
            //Playerタグを探す
            player = GameObject.FindGameObjectWithTag("Player");
            
        }


        private void Update()
        {
            //プレイヤーの方向を向く
            if (player != null)
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(direction);
            }

            //前進する
            transform.position += transform.forward * _moveSpeed * Time.deltaTime;
        }


        private void OnTriggerEnter(Collider other)
        {
            //プレイヤーに触れたら自身を破棄する
            if (other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
                _gameSystemFacade.SubtractScore(_subScoreValue);
                _seManager.PlaySE(SE.Damage);
            }

            //Bulletに触れたら自身と弾を破棄しスコアを加算する
            if (other.gameObject.CompareTag("Bullet"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
                _gameSystemFacade.AddScore(_addScoreValue);
                //エフェクトを再生する
                Instantiate(_destroyEffectPrefab, transform.position, Quaternion.identity);
                _seManager.PlaySE(SE.Destroy);
            }
        }
    }
}