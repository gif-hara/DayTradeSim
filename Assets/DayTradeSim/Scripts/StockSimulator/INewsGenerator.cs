namespace DayTradeSim.StockSimulator
{
    public interface INewsGenerator
    {
        News Generate(Company company);
    }
}
