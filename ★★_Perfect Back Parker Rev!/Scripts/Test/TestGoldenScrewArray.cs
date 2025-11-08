using PerfectBackParkerRev.GameCores.GameSystems.ItemPickUps;
using PerfectBackParkerRev.Utilities;
using UnityEngine;
using VContainer;

namespace Test
{
    /// <summary>
    /// テスト用：「金のネジ」取得の管理をテスト
    /// </summary>
    public class TestGoldenScrewArray : MonoBehaviour
    {
        [Inject] private readonly GoldenScrewStatus status;
        bool?[] a;

        private void Start()
        {
            a = status._goldenScrewStatusArray;
        }

        // Update is called once per frame
        void Update()
        {
            MyLogger.Log($"<color=green>「金のネジ」テスト：{a[0]}, {a[1]}, {a[2]}, {a[3]}, {a[4]}, </color>");
        }
    }
}