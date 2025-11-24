using Alchemy.Inspector;
using UnityEngine;

namespace SupernovaTrolley.Players
{
    /// <summary>
    /// バレット
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        [Title("各種パラメータ調整")]
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _lifetime = 15f;

        private void Update()
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
            //一定時間進んだら自身を破棄する
            Destroy(gameObject, _lifetime);
        }
    }
}