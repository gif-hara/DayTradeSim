namespace DayTradeSim.StockSimulator
{
    public abstract class News : INews
    {
        public string Title { get; }
        
        public string Content { get; }

        public abstract void Apply(Core core);

        protected News(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}
