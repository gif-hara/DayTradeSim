using System.Collections.Generic;

namespace DayTradeSim.StockSimulator
{
    public interface INewsCompaniesImpact
    {
        public void Apply(Core core, List<Company> companies);
    }
}
