using GiikutenApplication.PackScene.Data;
using GiikutenApplication.PackScene.Domain.Model;
using GiikutenApplication.Pop;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace GiikutenApplication.PackScene.Domain.Usecase
{
    /// <summary>
    /// ガチャを引いてアニメーションを再生する処理
    /// </summary>
    public class PackOpenUsecase
    {
        //対象のリポジトリ,Model
        [Inject] private readonly IPackSceneRepository _repository;
        [Inject] private readonly PackScreenModel _packScreenModel;
        [Inject] private readonly PopCanvas _cautionPop;
        [Inject] private readonly PackOpenAnimationCanvas _PackOpenAnimationCanvas;


        //要確認：ガチャを引くために必要なガチャ石の数
        private const int _STONE_PER_GACHA = 0;


        /// <summary>
        /// Usecase処理を実行
        /// </summary>
        /// <param name="isRandom">ランダムパックかどうか</param>
        public async Task Execute(bool isRandom)
        {
            Debug.Log($"<color=yellow>Usecase</color>:ガチャを引いてアニメーションを再生する処理");
            
            //ガチャを引けるかどうかチェック
            if (_packScreenModel.GachaStoneAmount.Value < _STONE_PER_GACHA)
            {
                Debug.Log($"<color=red>ガチャ石の数がたりません（{_STONE_PER_GACHA}個/回）</color>");
                Object.Instantiate(_cautionPop);
                return;
            }

            //ガチャ石を消費して更新
            var sendDTO = new PackScreenDTO(_packScreenModel.GachaStoneAmount.Value - _STONE_PER_GACHA);
            var clear = await _repository.Save(sendDTO);
            if (!clear)
            {
                Debug.LogError($"サーバへの書き込みに失敗");
                return;
            }
            _packScreenModel.GachaStoneAmount.Value -= _STONE_PER_GACHA;


            //抽選結果をサーバから取得
            string name = "";
            string job = "";
            string advice = "";
            if (isRandom)
            {
                Debug.Log($"ランダムパックを引きます");
                var dto = await _repository.FetchRandomPack();
                name = dto.RandomUserName;
                job = dto.RandomCharacterType;
            }
            else
            {
                Debug.Log($"レコメンドパックを引きます");
                var dto = await _repository.FetchRecommendPack();
                name = dto.RecommendUserName;
                job = dto.RecommendCharacterType;
                advice = dto.Advice;
            }

            //アニメーションを再生
            var obj = Object.Instantiate(_PackOpenAnimationCanvas);
            obj.UserName = name;
            obj.JobName = job;
            obj.Advice = advice;          
        }
    }
}
