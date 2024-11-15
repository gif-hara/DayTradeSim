using System.Collections.Generic;
using System.Linq;

namespace DayTradeSim.StockSimulator
{
    public sealed class NewsCompanies : News
    {
        private readonly List<int> companyIds;
        
        private readonly List<INewsImpactCompanies> impacts;

        public NewsCompanies(string title, string content, List<int> companyIds, List<INewsImpactCompanies> impacts)
            : base(title, content)
        {
            this.companyIds = companyIds;
            this.impacts = impacts;
        }

        public override void Apply(Core core)
        {
            var companies = companyIds.Select(core.GetCompany).ToList();
            foreach (var impact in impacts)
            {
                impact.Apply(core, companies);
            }
        }
    }
}
