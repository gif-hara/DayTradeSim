namespace DayTradeSim.StockSimulator
{
    public interface INewsGenerator
    {
        INews Generate(Core core);
    }
}
