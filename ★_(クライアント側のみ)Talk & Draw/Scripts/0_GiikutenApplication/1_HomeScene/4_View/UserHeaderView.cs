using GiikutenApplication.HomeScene.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace GiikutenApplication.HomeScene.View
{
    /// <summary>
    /// 画面上部のユーザ情報を表示するヘッダーView
    /// </summary>
    public class UserHeaderView : MonoBehaviour, IUserHeaderView
    {
        //各UI要素
        [SerializeField] private Text _userNameText; //ユーザの名前
        [SerializeField] private Text _jobNameText; //ユーザのジョブ名
        [SerializeField] private Text _gachaStoneAmountText; //ガチャ石の所持数


        public void UpdateUserName(string userName)
        {
            _userNameText.text = userName;
        }


        public void UpdateJobName(string jobName)
        {
            _jobNameText.text = jobName;
        }


        public void UpdateGachaStoneAmount(int gachaStoneAmount)
        {
            _gachaStoneAmountText.text = gachaStoneAmount.ToString();
        }
    }
}