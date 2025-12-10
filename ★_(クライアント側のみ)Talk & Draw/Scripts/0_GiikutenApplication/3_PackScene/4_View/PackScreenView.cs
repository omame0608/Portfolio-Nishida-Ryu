using GiikutenApplication.PackScene.Presentation.IView;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UtilView;

namespace GiikutenApplication.PackScene.View
{
    /// <summary>
    /// Pack画面で利用するデータView
    /// </summary>
    public class PackScreenView : MonoBehaviour, IPackScreenView
    {
        //各UI要素
        [SerializeField] private Text _gachaStoneAmount;
        [SerializeField] private ClickableButton _randomPackButton;
        [SerializeField] private ClickableButton _recommendPackButton;


        //ガチャ石
        public void UpdateGachaStoneAmount(int amount) => _gachaStoneAmount.text = amount.ToString();

        //ランダムパックを引くとき発火
        public IObservable<Unit> OnClickRandomPack => _randomPackButton.OnClickSubject;

        //レコメンドパックを引くとき発火
        public IObservable<Unit> OnClickRecommendPack => _recommendPackButton.OnClickSubject;
    }
}