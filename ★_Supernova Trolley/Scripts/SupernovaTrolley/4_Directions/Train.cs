using Alchemy.Inspector;
using UnityEngine;

namespace SupernovaTrolley.Directions
{
    /// <summary>
    /// 電車
    /// </summary>
    public class Train : MonoBehaviour
    {
        [Title("各種パラメータ調整")]
        [SerializeField] private float _moveSpeed; //移動速度


        private void Update()
        {
            //移動:x+方向
            var delta = new Vector3(-_moveSpeed * Time.deltaTime, _moveSpeed * Time.deltaTime * Mathf.Tan(15*Mathf.Deg2Rad), 0f);
            transform.localPosition += delta;
        }
    }
}