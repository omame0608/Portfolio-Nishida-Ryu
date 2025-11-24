using Alchemy.Inspector;
using DG.Tweening;
using UnityEngine;

namespace SupernovaTrolley.Directions
{
    /// <summary>
    /// スカイボックスシステム
    /// </summary>
    public class SkyboxSystem : MonoBehaviour
    {
        [Title("操作対象のマテリアル")]
        private Material _skyboxMaterial;
        [SerializeField] private Light _directionalLight;

        [Title("各種パラメータ調整")]
        [SerializeField] private bool _linkWithLight = true;
        [SerializeField] private float _startExposure;
        [SerializeField] private float _goalExposure;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _supernovaEase;
        [SerializeField] private float _startIntensity;
        [SerializeField] private float _goalIntensity;

        //キャンセル用
        private Tweener _SkyTweener;
        private Tweener _lightTweener;


        private void Start()
        {
            //現在のスカイボックスを取得
            _skyboxMaterial = RenderSettings.skybox;
        }


        /// <summary>
        /// 超新星爆発スカイボックスを開始する
        /// </summary>
        public void StartSupernova()
        {
            _SkyTweener?.Kill();
            _SkyTweener = DOTween.To(() => _startExposure, x => _skyboxMaterial.SetFloat("_Exposure", x), _goalExposure, _duration)
                .SetEase(_supernovaEase)
                .SetLink(gameObject);

            //ライトと連動する場合
            if (_linkWithLight)
            {
                _lightTweener?.Kill();
                _SkyTweener = DOTween.To(() => _startIntensity, x => _directionalLight.intensity = x, _goalIntensity, _duration)
                                     .SetEase(_supernovaEase)
                                     .SetLink(gameObject);
            }
        }


        private void OnDestroy()
        {
            //スカイボックスを元に戻す
            _skyboxMaterial.SetFloat("_Exposure", _startExposure);
        }


        [Title("デバッグ用")]
        [Button]
        private void TestStartSupernova()
        {
            if (!Application.isPlaying) return;
            StartSupernova();
        }
    }
}