using System.Collections.Generic;

namespace DayTradeSim.StockSimulator
{
    public class Company
    {
        public int Id { get; private set; }
        
        public string Name { get; private set; }
        
        public float StockPrice { get; private set; }
        
        public List<Define.CompanyCategory> Categories { get; } = new();
        
        public float StockPriceDownFluctuation { get; private set; }
        
        public float StockPriceUpFluctuation { get; private set; }
        
        public Company(
            int id,
            string name,
            float stockPrice,
            List<Define.CompanyCategory> defaultCategories,
            float stockPriceDownFluctuation,
            float stockPriceUpFluctuation
            )
        {
            Id = id;
            Name = name;
            StockPrice = stockPrice;
            Categories.AddRange(defaultCategories);
            StockPriceDownFluctuation = stockPriceDownFluctuation;
            StockPriceUpFluctuation = stockPriceUpFluctuation;
        }

        public void Update()
        {
            StockPrice += UnityEngine.Random.Range(
                -StockPrice * StockPriceDownFluctuation,
                StockPrice * StockPriceUpFluctuation
                );
        }
    }
}
