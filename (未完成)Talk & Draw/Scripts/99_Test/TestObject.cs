using GiikutenApplication.HomeScene.Domain;
using GiikutenApplication.HomeScene.Domain.Model;
using GiikutenApplication.HomeScene.Domain.Usecase;
using UniRx;
using UnityEngine;
using VContainer;
using Cysharp.Threading.Tasks;

namespace Test
{
    /// <summary>
    /// テスト用：テスト書く用Monoクラス
    /// </summary>
    public class TestObject : MonoBehaviour
    {
        [Inject] private readonly IHomeSceneRepository _repository;
        [Inject] private readonly UserHeaderModel _userHeaderModel;
        [Inject] private readonly HomeSceneLoadUsecase _homeSceneLoadUsecase;
        
        private void Start()
        {
            //Model・サーバ更新
            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Q))
                .Subscribe(async _ =>
                {
                    //先にサーバに送信したほうがいいかも
                    _userHeaderModel.UserName.Value = "Reiya";
                    _userHeaderModel.JobName.Value = "Priest";
                    _userHeaderModel.GachaStoneAmount.Value = 9999;
                    var flag = await _repository.Save(_userHeaderModel.ConvertModelToDTO());
                    if (!flag) Debug.LogError($"データ送信エラー"); 
                }).AddTo(this);
            //Model・サーバ更新
            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.W))
                .Subscribe(async _ =>
                {
                    //先にサーバに送信したほうがいいかも
                    _userHeaderModel.UserName.Value = "Ryu";
                    _userHeaderModel.JobName.Value = "Mage";
                    _userHeaderModel.GachaStoneAmount.Value = 10;
                    var flag = await _repository.Save(_userHeaderModel.ConvertModelToDTO());
                    if (!flag) Debug.LogError($"データ送信エラー");
                }).AddTo(this);
            //シーンリロードユースケース
            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Space))
                .Subscribe(_ =>
                {
                    _homeSceneLoadUsecase.Execute().Forget();
                }).AddTo(this);
        }


        private void Update()
        {
            
        }
    }
}