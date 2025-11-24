using Alchemy.Inspector;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace SupernovaTrolley.Directions
{
    /// <summary>
    /// レールスクロールシステム
    /// </summary>
    public class RailScrollSystem : MonoBehaviour
    {
        [Title("操作対象のオブジェクト")]
        [SerializeField] private List<Transform> _railList;

        [Title("各種パラメータ調整")]
        [SerializeField, ReadOnly] private float _scrollSpeed = 0f; //スクロール速度
        private const float _RAIL_LENGTH = 45f; //レールの長さ
        private const float _RAIL_COUNT = 11f;   //レールの数
        private const float _THRESHOLD_Z = -230f; //再スクロールを行うZ座標の閾値
        [SerializeField] private float _accelDuration; //加速にかかる時間
        [SerializeField] private float _goalSpeed; //目標速度
        [SerializeField] private Ease _accelEase_Start; //加速のイージング
        [SerializeField] private Ease _accelEase_End;   //減速のイージング

        //システムを利用するか
        private bool _isActive = true;

        //キャンセル用
        private Tweener _tweener;


        /// <summary>
        /// レールスクロールシステムを使用開始する
        /// </summary>
        public void UseRailScrollSystem()
        {
            _isActive = true;
            _tweener?.Kill();
            _tweener = DOTween.To(() => 0, x => _scrollSpeed = x, _goalSpeed, _accelDuration)
                              .SetEase(_accelEase_Start)
                              .SetLink(gameObject);
        }


        /// <summary>
        /// レールスクロールシステムを使用終了する
        /// </summary>
        public void releaseRailScrollSystem()
        {
            _tweener?.Kill();
            _tweener = DOTween.To(() => _scrollSpeed, x => _scrollSpeed = x, 0f, _accelDuration)
                              .SetEase(_accelEase_End)
                              .OnComplete(() => _isActive = false)
                              .SetLink(gameObject);
        }


        private void Update()
        {
            if (!_isActive) return;

            //スクロール処理
            foreach (var rail in _railList)
            {
                rail.localPosition += Vector3.back * _scrollSpeed * Time.deltaTime;

                //閾値を超えたらスクロール開始位置まで戻す
                if (rail.localPosition.z <= _THRESHOLD_Z)
                {
                    rail.localPosition += Vector3.forward * _RAIL_LENGTH * _RAIL_COUNT;
                }
            }
        }


        [Title("デバッグ用")]
        [Button]
        private void TestUseRailScrollSystem()
        {
            if (!Application.isPlaying) return;
            UseRailScrollSystem();
        }
        [Button]
        private void TestReleaseRailScrollSystem()
        {
            if (!Application.isPlaying) return;
            releaseRailScrollSystem();
        }
    }
}