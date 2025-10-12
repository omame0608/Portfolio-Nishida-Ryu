using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

/// <summary>
/// ゲームシーンのDIコンテナ
/// </summary>
/// <author>西田琉</author>
public class GameLifetimeScope : LifetimeScope
{
    //MonoBehaviour継承インスタンスの登録
    [SerializeField] private GameViewController _gameViewController; //ゲーム進行のView制御用
    [SerializeField] private TrolleyDIResolver _trolleyDIResolver; //TrolleyのDI解決用
    [SerializeField] private QuizSystemFacade _quizSystemFacade; //クイズ窓口
    [SerializeField] private LogFactory _logFactory; //ログ生成用
    [SerializeField] private CoinFactory _coinFactory; //コイン生成用
    [SerializeField] private FieldScrollSystem _fieldScrollSystemFacade; //フィールド操作用
    [SerializeField] private SEManager _seManager; //SE管理クラス
    [SerializeField] private BGMManager _bgmManager; //BGM管理クラス


    protected override void Configure(IContainerBuilder builder)
    {
        //pureC#の登録
        builder.RegisterEntryPoint<GameSystem>(Lifetime.Singleton).AsSelf();
        builder.Register<ITimerSystemFacade, TimerSystemFacade>(Lifetime.Singleton);

        //MonoBehaviourの登録
        builder.RegisterComponent(_gameViewController);
        builder.RegisterComponent(_trolleyDIResolver);
        builder.RegisterComponent(_quizSystemFacade).As<IQuizSystemFacade>();
        builder.RegisterComponent(_logFactory);
        builder.RegisterComponent(_coinFactory);
        builder.RegisterComponent(_fieldScrollSystemFacade).As<IFieldScrollSystemFacade>();
        builder.RegisterComponent(_seManager);
        builder.RegisterComponent(_bgmManager);
    }
}
