using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

/// <summary>
/// TrolleyController生成時の初期化を行うクラス
/// PlayerInputManagerと2つで実質的なファクトリの働きをしてる
/// </summary>
/// <author>西田琉</author>
public class TrolleyDIResolver : MonoBehaviour
{
    //リゾルバ
    [Inject] private IObjectResolver _objectResolver;

    //参照
    [Inject] private GameSystem _gameSystem;


    async void Awake()
    {
        //PlayerInputManagerが準備できるまで待機
        await UniTask.WaitUntil(() => PlayerInputManager.instance != null);

        //player(TrolleyController)生成時の初期化処理を設定
        PlayerInputManager.instance.onPlayerJoined += playerInput =>
        {
            //依存関係を解決
            _objectResolver.InjectGameObject(playerInput.gameObject);

            //ゲームシステムにプレイヤーを登録
            _gameSystem.TrolleyList.Add(playerInput.GetComponent<TrolleyController>());
        };
    }
}
