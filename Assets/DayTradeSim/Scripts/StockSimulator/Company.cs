using System;

namespace DayTradeSim.StockSimulator
{
    public class Company
    {
        public float StockPrice { get; private set; }
        
        public Company(float stockPrice)
        {
            StockPrice = stockPrice;
        }

        public void Update()
        {
            StockPrice += UnityEngine.Random.Range(-1.0f, 1.0f);
        }
    }
}
