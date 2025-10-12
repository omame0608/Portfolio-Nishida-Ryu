using System;
using UniRx;
using UnityEngine;
using UtilView;

namespace GiikutenApplication.Common.BackToHomeButton
{
    /// <summary>
    /// PackSceneからHomeSceneへ戻るボタンView
    /// </summary>
    public class BackToHomeButtonView : MonoBehaviour, IBackToHomeButtonView
    {
        //各UI要素
        [SerializeField] private ClickableButton _backButton; //HomeSceneへ戻るボタン


        public IObservable<Unit> OnBackButtonClick => _backButton.OnClickSubject;
    }
}