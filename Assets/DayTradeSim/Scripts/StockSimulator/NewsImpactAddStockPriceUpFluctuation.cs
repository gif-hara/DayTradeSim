using System;
using System.Collections.Generic;
using UnityEngine;

namespace DayTradeSim.StockSimulator
{
    [Serializable]
    public class NewsImpactAddStockPriceUpFluctuation : INewsImpactCompanies
    {
        [SerializeField]
        private float min;

        [SerializeField]
        private float max;
        
        public void Apply(Core core, List<Company> companies)
        {
            foreach (var company in companies)
            {
                company.AddStockPriceUpFluctuation(UnityEngine.Random.Range(min, max));
            }
        }
    }
}
