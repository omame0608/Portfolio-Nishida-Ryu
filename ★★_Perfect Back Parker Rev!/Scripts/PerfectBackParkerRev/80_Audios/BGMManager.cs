using DG.Tweening;
using UnityEngine;

namespace PerfectBackParkerRev.Audios
{
    /// <summary>
    /// BGMマネージャー
    /// </summary>
    public class BGMManager : MonoBehaviour
    {
        //キャッシュ
        private AudioSource _audioSource;

        //BGMのクリップ一覧
        [SerializeField] private AudioClip _opening; //オープニング
        [SerializeField] private AudioClip _stageSelect; //ステージセレクト
        [SerializeField] private AudioClip _stage1; //Stage1
        [SerializeField] private AudioClip _stage2; //Stage2
        [SerializeField] private AudioClip _stage3; //Stage3

        //制御用
        private Tweener _fadeTween; //フェード用シーケンス


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            //BGMマネージャーはシーン遷移で破棄されないようにする
            DontDestroyOnLoad(gameObject);
        }


        /// <summary>
        /// 指定したBGMを再生する
        /// </summary>
        /// <param name="bgm">再生するBGM</param>
        public void PlayBGM(BGM bgm)
        {
            _fadeTween?.Kill(true);

            //現在再生中のBGMを停止
            _audioSource.Stop();

            //指定されたSEを設定
            switch (bgm)
            {
                //オープニング
                case BGM.Opening:
                    _audioSource.clip = _opening;
                    _audioSource.volume = 1f;
                    _audioSource.Play();
                    break;
                //ステージセレクト
                case BGM.StageSelect:
                    _audioSource.clip = _stageSelect;
                    _audioSource.volume = 0.5f;
                    _audioSource.Play();
                    break;
                //Stage1
                case BGM.Stage1:
                    _audioSource.clip = _stage1;
                    _audioSource.volume = 0.25f;
                    _audioSource.Play();
                    break;
                //Stage2
                case BGM.Stage2:
                    _audioSource.clip = _stage2;
                    _audioSource.volume = 0.3f;
                    _audioSource.Play();
                    break;
                //Stage3
                case BGM.Stage3:
                    _audioSource.clip = _stage3;
                    _audioSource.volume = 0.25f;
                    _audioSource.Play();
                    break;
            }
        }


        /// <summary>
        /// BGMを停止する
        /// </summary>
        /// <param name="fadeVolume">フェードアウトして止めるか</param>
        public void StopBGM(bool fadeVolume = false)
        {
            if (fadeVolume)
            {
                _fadeTween = DOTween.To(() => _audioSource.volume, x => _audioSource.volume = x, 0f, 1f)
                    .OnComplete(() =>
                    {
                        _audioSource.Stop();
                    });
            }
            else
            {
                _audioSource.Stop();
            }
        }


        /// <summary>
        /// BGMを一時停止する
        /// </summary>
        public void PauseBGM()
        {
            _audioSource.Pause();
        }


        /// <summary>
        /// BGMを再開する
        /// </summary>
        public void UnPauseBGM()
        {
            _audioSource.UnPause();
        }
    }


    /// <summary>
    /// BGMの種類
    /// </summary>
    public enum BGM
    {
        Opening, //オープニング
        StageSelect, //ステージセレクト
        Stage1, //Stage1
        Stage2, //Stage2
        Stage3, //Stage3
    }
}