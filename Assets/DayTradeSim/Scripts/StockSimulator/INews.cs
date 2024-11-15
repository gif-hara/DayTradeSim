namespace DayTradeSim.StockSimulator
{
    public interface INews
    {
        public string Title { get; }
        
        public string Content { get; }
        
        /// <summary>
        /// ニュースの効果を適用する
        /// </summary>
        public void Apply(Core core);
    }
}
