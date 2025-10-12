using Cysharp.Threading.Tasks;
using GiikutenApplication.PackScene.Data;
using UnityEngine;

namespace GiikutenApplication.Common
{
    /// <summary>
    /// URLからImageに変換する
    /// 
    /// </summary>
    public class OnlineImageGetter
    {
        /*
         * 
         * 
         * 現状はResourcesフォルダ内のテスト用画像を持ってくるだけのクラス
         * 本番用に書き換えてください
         * 
         * 
         */
        

        /// <summary>
        /// URLから画像を取得する
        /// 現在テスト仕様
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>画像スプライト</returns>
        public async UniTask<Sprite> GetImageWithURL(string url)
        {
            //実際のサーバ通信には時間がかかる
            await UniTask.Delay(100);

            //失敗ならnull
            //if (_isReadFailed) return null;

            Sprite image;
            switch (url)
            {
                case "https1": image = Resources.Load<Sprite>($"TestIcons/UserIcon1"); break;
                case "https2": image = Resources.Load<Sprite>($"TestIcons/UserIcon2"); break;
                case "https3": image = Resources.Load<Sprite>($"TestIcons/UserIcon3"); break;
                case "https4": image = Resources.Load<Sprite>($"TestIcons/UserIcon4"); break;
                case "https5": image = Resources.Load<Sprite>($"TestIcons/UserIcon5"); break;
                case "https//topic1": image = Resources.Load<Sprite>($"TestIcons/TopicIcon1"); break;
                case "https//topic2": image = Resources.Load<Sprite>($"TestIcons/TopicIcon2"); break;
                case "https//topic3": image = Resources.Load<Sprite>($"TestIcons/TopicIcon3"); break;
                case "https//topic4": image = Resources.Load<Sprite>($"TestIcons/TopicIcon4"); break;
                case "https//topic5": image = Resources.Load<Sprite>($"TestIcons/TopicIcon5"); break;
                default: image = Resources.Load<Sprite>($"TestIcons/None"); break;
            }

            return image;
        }
    }
}