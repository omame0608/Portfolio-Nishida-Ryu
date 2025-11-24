using UnityEngine;

namespace SupernovaTrolley.Audios
{
    /// <summary>
    /// BGM管理クラス
    /// </summary>
    public class BGMManager : MonoBehaviour
    {
        //キャッシュ
        private AudioSource[] _audioSource;
        private AudioSource _forEnvironment; //環境音用
        private AudioSource _forBGM; //BGM用

        //BGMのクリップ一覧
        [SerializeField] private AudioClip _environment; //環境音
        [SerializeField] private AudioClip _normalBGM; //通常時BGM


        private void Awake()
        {
            _audioSource = GetComponents<AudioSource>();
            _forEnvironment = _audioSource[0];
            _forBGM = _audioSource[1];
        }


        /// <summary>
        /// 指定したBGMを再生する
        /// </summary>
        /// <param name="bgm">再生するBGM</param>
        public void PlayBGM(BGM bgm)
        {
            //指定されたSEを設定
            switch (bgm)
            {
                //環境音
                case BGM.Environment:
                    _forEnvironment.clip = _environment;
                    _forEnvironment.volume = 0.3f;
                    _forEnvironment.Play();
                    break;
                //通常時BGM
                case BGM.NormalBGM:
                    _forBGM.clip = _normalBGM;
                    _forBGM.Play();
                    break;
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public enum BGM
    {
        Environment, //環境音
        NormalBGM, //通常時BGM
    }
}