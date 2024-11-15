using System.Collections.Generic;

namespace DayTradeSim.StockSimulator
{
    public interface INewsImpactCompanies
    {
        public void Apply(Core core, List<Company> companies);
    }
}
