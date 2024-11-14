using System.Collections.Generic;

namespace DayTradeSim.StockSimulator
{
    public class Core
    {
        public List<Company> Companies { get; } = new();
        
        private int companyId = 1001;

        public float Money { get; private set; }

        public Dictionary<int, int> BuyList { get; } = new();

        public enum BuyResult
        {
            Success,
            NotEnoughMoney,
            NotFoundCompany,
        }
        
        public Core()
        {
            AddCompany("A", 100.0f);
            AddCompany("B", 200.0f);
            AddCompany("C", 300.0f);
            Money = 1000000.0f;
        }

        public void Update()
        {
            foreach (var i in Companies)
            {
                i.Update();
            }
        }
        
        public BuyResult Buy(int companyId, int quantity)
        {
            var company = Companies.Find(x => x.Id == companyId);
            if (company == null)
            {
                return BuyResult.NotFoundCompany;
            }
            var price = company.StockPrice * quantity;
            if (Money < price)
            {
                return BuyResult.NotEnoughMoney;
            }
            Money -= price;
            if (!BuyList.TryAdd(companyId, quantity))
            {
                BuyList[companyId] += quantity;
            }
            return BuyResult.Success;
        }
        
        public void AddCompany(string name, float stockPrice)
        {
            Companies.Add(new Company(companyId++, name, stockPrice));
        }
        
        public Company GetCompany(int companyId)
        {
            return Companies.Find(x => x.Id == companyId);
        }
    }
}
