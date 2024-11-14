using System.Collections.Generic;
using System.Linq;

namespace DayTradeSim.StockSimulator
{
    public class Core
    {
        public List<Company> Companies { get; } = new();
        
        private int companyId = 1001;

        public float Money { get; private set; }
        
        /// <summary>
        /// 元本
        /// </summary>
        public float Principal { get; private set; }
        
        public float Portfolio => Money + BuyList.Sum(x =>
        {
            var company = GetCompany(x.Key);
            if(company == null)
            {
                return 0.0f;
            }
            return company.StockPrice * x.Value;
        });
        
        public float PortfolioRate => (Portfolio - Principal) / Principal * 100.0f;

        public Dictionary<int, int> BuyList { get; } = new();

        public enum BuyResult
        {
            Success,
            NotEnoughMoney,
            NotFoundCompany,
        }
        
        public enum SellResult
        {
            Success,
            NotFoundCompany,
            NotEnoughStock,
            NotPossessionStock,
        }
        
        public Core()
        {
            AddCompany("A", 1000.0f);
            AddCompany("B", 2000.0f);
            AddCompany("C", 3000.0f);
            AddPrincipal(1000000.0f);
        }

        public void Update()
        {
            foreach (var i in Companies)
            {
                i.Update();
            }
        }
        
        public void AddPrincipal(float value)
        {
            Money += value;
            Principal += value;
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
        
        public SellResult Sell(int companyId, int quantity)
        {
            var company = Companies.Find(x => x.Id == companyId);
            if (company == null)
            {
                return SellResult.NotFoundCompany;
            }
            if (!BuyList.ContainsKey(companyId))
            {
                return SellResult.NotPossessionStock;
            }
            if (BuyList[companyId] < quantity)
            {
                return SellResult.NotEnoughStock;
            }
            var price = company.StockPrice * quantity;
            Money += price;
            BuyList[companyId] -= quantity;
            if (BuyList[companyId] == 0)
            {
                BuyList.Remove(companyId);
            }
            return SellResult.Success;
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
