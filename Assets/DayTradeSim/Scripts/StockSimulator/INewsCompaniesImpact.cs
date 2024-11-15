using System.Collections.Generic;

namespace DayTradeSim.StockSimulator
{
    public interface INewsCompaniesImpact
    {
        public void Apply(List<Company> companies);
    }
}
