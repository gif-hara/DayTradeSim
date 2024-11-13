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
            StockPrice += UnityEngine.Random.Range(-1.0f, 1.0f);
        }
    }
}
