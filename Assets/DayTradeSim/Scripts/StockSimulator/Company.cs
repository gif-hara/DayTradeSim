using System;

namespace DayTradeSim.StockSimulator
{
    public class Company
    {
        public int Id { get; private set; }
        
        public string Name { get; private set; }
        
        public float StockPrice { get; private set; }
        
        public Company(int id, string name, float stockPrice)
        {
            Id = id;
            Name = name;
            StockPrice = stockPrice;
        }

        public void Update()
        {
            var rate = StockPrice / 100.0f;
            StockPrice += UnityEngine.Random.Range(-rate, rate);
        }
    }
}
