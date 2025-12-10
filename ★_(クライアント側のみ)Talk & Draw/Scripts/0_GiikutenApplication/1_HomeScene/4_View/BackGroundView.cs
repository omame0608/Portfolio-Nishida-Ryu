using GiikutenApplication.HomeScene.Presentation.IView;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GiikutenApplication.HomeScene.View
{
    /// <summary>
    /// HomeSceneの背景View
    /// </summary>
    public class BackGroundView : MonoBehaviour, IBackGroundView
    {
        //背景画像一覧
        [SerializeField] private List<Sprite> _sprites = new();


        public void SetBackGroundImage(string jobName)
        {
            var _image = GetComponent<Image>();
            switch (jobName)
            {
                case "Swordsman": _image.sprite = _sprites[0]; break;
                case "Mage": _image.sprite = _sprites[1]; break;
                case "Knight": _image.sprite = _sprites[2]; break;
                case "Ninja": _image.sprite = _sprites[3]; break;
                case "Thief": _image.sprite = _sprites[4]; break;
                case "Archer": _image.sprite = _sprites[5]; break;
                case "Clown": _image.sprite = _sprites[6]; break;
                case "Berserker": _image.sprite = _sprites[7]; break;
                case "Bard": _image.sprite = _sprites[8]; break;
                case "Alchemist": _image.sprite = _sprites[9]; break;
                case "Priest": _image.sprite = _sprites[10]; break;
                default: _image.sprite = null; break;
            }
        }
    }
}