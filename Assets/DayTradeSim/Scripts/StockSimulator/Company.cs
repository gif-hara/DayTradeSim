using System.Collections.Generic;

namespace DayTradeSim.StockSimulator
{
    public class Company
    {
        public int Id { get; private set; }
        
        public string Name { get; private set; }
        
        public float StockPrice { get; private set; }
        
        public List<Define.CompanyCategory> Categories { get; } = new();
        
        public Company(int id, string name, float stockPrice, List<Define.CompanyCategory> defaultCategories)
        {
            Id = id;
            Name = name;
            StockPrice = stockPrice;
            Categories.AddRange(defaultCategories);
        }

        public void Update()
        {
            var rate = StockPrice / 100.0f;
            StockPrice += UnityEngine.Random.Range(-rate, rate);
        }
    }
}
