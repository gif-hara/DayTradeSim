using System.Collections.Generic;

namespace DayTradeSim.StockSimulator
{
    public class Core
    {
        public List<Company> Companies { get; } = new();
        
        public Core()
        {
            Companies.Add(new Company(100.0f));
            Companies.Add(new Company(200.0f));
            Companies.Add(new Company(300.0f));
        }
    }
}
