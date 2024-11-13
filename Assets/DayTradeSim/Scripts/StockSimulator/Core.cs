using System.Collections.Generic;

namespace DayTradeSim.StockSimulator
{
    public class Core
    {
        public List<Company> Companies { get; } = new();
        
        private int companyId = 1001;
        
        public Core()
        {
            AddCompany("A", 100.0f);
            AddCompany("B", 200.0f);
            AddCompany("C", 300.0f);
        }

        public void Update()
        {
            foreach (var i in Companies)
            {
                i.Update();
            }
        }
        
        public void AddCompany(string name, float stockPrice)
        {
            Companies.Add(new Company(companyId++, name, stockPrice));
        }
    }
}
