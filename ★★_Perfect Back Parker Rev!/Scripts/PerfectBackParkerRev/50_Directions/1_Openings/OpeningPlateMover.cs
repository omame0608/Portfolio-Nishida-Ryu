using UnityEngine;

namespace PerfectBackParkerRev.Directions.Openings
{
    /// <summary>
    /// 演出用：オープニングのプレートの動きを制御する
    /// </summary>
    public class OpeningPlateMover : MonoBehaviour
    {
        //破棄タイミング調整用
        private const float _PLATE_WIDTH = 89.8f; //プレートの幅
        private const float _GOAL_X = -_PLATE_WIDTH * 2; //破棄するX座標
        private const float _START_X = _PLATE_WIDTH; //開始するX座標
        private const float _SECOND_PER_PLATE = 5f; //プレート1枚分を通過するのにかかる秒数
        private const float _OFFSET_X = 0.1f; //破棄位置の調整用オフセット


        private void Update()
        {
            //左方向に移動し、一定位置を超えたら開始位置に戻す
            transform.Translate(Vector3.left * (_PLATE_WIDTH / _SECOND_PER_PLATE) * Time.deltaTime);
            if (transform.position.x <= _GOAL_X + _OFFSET_X)
            {
                transform.position = new Vector3(_START_X, transform.position.y, transform.position.z);
            }
        }
    }
}
