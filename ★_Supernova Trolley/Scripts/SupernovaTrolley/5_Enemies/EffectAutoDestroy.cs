using Alchemy.Inspector;
using UnityEngine;

namespace SupernovaTrolley.Enemies
{
    /// <summary>
    /// エフェクトを自動で破棄するコンポーネント
    /// </summary>
    public class EffectAutoDestroy : MonoBehaviour
    {
        [Title("各種パラメータ調整")]
        [SerializeField] private float _lifetime;


        private void Start()
        {
            //一定時間後に自身を破棄する
            Destroy(gameObject, _lifetime);
        }
    }
}