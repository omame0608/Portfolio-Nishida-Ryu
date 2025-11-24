using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SupernovaTrolley.Audios
{
    /// <summary>
    /// SE管理クラス
    /// </summary>
    public class SEManager : MonoBehaviour
    {
        //SEのクリップ一覧
        [SerializeField] private AudioClip _shoot;
        [SerializeField] private AudioClip _destroy;
        [SerializeField] private AudioClip _damage;


        /// <summary>
        /// 指定したSEを再生する
        /// </summary>
        /// <param name="se">再生するSE</param>
        public async void PlaySE(SE se)
        {
            //SE再生用のAudioSourceを動的に作成
            var source = gameObject.AddComponent<AudioSource>();

            //指定されたSEを設定
            switch (se)
            {
                //クラクション
                case SE.Shoot:
                    source.clip = _shoot;
                    source.volume = 0.5f;
                    break;
                //カチカチ1
                case SE.Destroy:
                    source.clip = _destroy;
                    source.volume = 1f;
                    break;
                //カチカチ2
                case SE.Damage:
                    source.clip = _damage;
                    source.volume = 1f;
                    break;
            }

            //SEを再生し、終わったらAudioSourceを削除
            source.Play();
            await UniTask.WaitUntil(() => !source.isPlaying);
            Destroy(source);
        }
    }


    /// <summary>
    /// SE選択用
    /// </summary>
    public enum SE
    {
        Shoot,
        Destroy,
        Damage
    }
}