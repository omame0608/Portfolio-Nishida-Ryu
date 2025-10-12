using VContainer;
using VContainer.Unity;
using UnityEngine;

/// <summary>
/// DIƒRƒ“ƒeƒi
/// </summary>
public class GameLifetimeScope : LifetimeScope
{
    //Prefab‚ÌQÆ
    [SerializeField] private Connection _connectionPrefab;
    [SerializeField] private NodeAND _nodeAND;
    [SerializeField] private NodeOR _nodeOR;
    [SerializeField] private NodeNOT _nodeNOT;
    [SerializeField] private NodeIn _nodeIn;
    [SerializeField] private NodeOut _nodeOut;

    //MonoBehaviour‚ÌQÆ


    protected override void Configure(IContainerBuilder builder)
    {
        //pureC#‚Ì“o˜^
        builder.Register<IConnectionFactory, ConnectionFactory>(Lifetime.Singleton);
        builder.Register<INodeFactory, NodeFactory>(Lifetime.Singleton);

        //MonoBehaviour‚Ì“o˜^
        builder.RegisterComponent(_connectionPrefab);
        builder.RegisterComponent(_nodeAND);
        builder.RegisterComponent(_nodeOR);
        builder.RegisterComponent(_nodeNOT);
        builder.RegisterComponent(_nodeIn);
        builder.RegisterComponent(_nodeOut);
    }
}
