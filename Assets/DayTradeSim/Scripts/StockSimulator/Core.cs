using System.Collections.Generic;

namespace DayTradeSim.StockSimulator
{
    public class Core
    {
        public List<Company> Companies { get; } = new();
        
        public Core()
        {
            Companies.Add(new Company("A", 100.0f));
            Companies.Add(new Company("B", 200.0f));
            Companies.Add(new Company("C", 300.0f));
        }

        public void Update()
        {
            foreach (var i in Companies)
            {
                i.Update();
            }
        }
    }
}
