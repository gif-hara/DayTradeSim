using System;

namespace DayTradeSim.StockSimulator
{
    public class Company
    {
        public string Name { get; private set; }
        
        public float StockPrice { get; private set; }
        
        public Company(string name, float stockPrice)
        {
            Name = name;
            StockPrice = stockPrice;
        }

        public void Update()
        {
            StockPrice += UnityEngine.Random.Range(-1.0f, 1.0f);
        }
    }
}
