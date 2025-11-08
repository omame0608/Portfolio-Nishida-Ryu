namespace PerfectBackParkerRev.GameCores.GameSystems.ItemPickUps
{
    /// <summary>
    /// 「金のネジ」検知者をロジックへ登録するためのインターフェース
    /// </summary>
    public interface IGoldenScrewGetNotifierRegistrar
    {
        /// <summary>
        /// 「金のネジ」通知者を追加
        /// </summary>
        /// <param name="notifier">「金のネジ」通知者</param>
        void AddNotifier(IGoldenScrewGetNotifier notifier);
    }
}