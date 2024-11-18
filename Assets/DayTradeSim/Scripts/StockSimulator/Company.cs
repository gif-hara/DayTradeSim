using System;
using System.Collections.Generic;
using UnityEngine;

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
            StockPrice = (float)Math.Round(stockPrice, 2);
            Categories.AddRange(defaultCategories);
            StockPriceDownFluctuation = stockPriceDownFluctuation;
            StockPriceUpFluctuation = stockPriceUpFluctuation;
        }

        public void Update()
        {
            StockPrice += UnityEngine.Random.Range(
                -StockPriceDownFluctuation,
                StockPriceUpFluctuation
                );
            StockPrice = (float)Math.Round(StockPrice, 2);
        }
        
        public void AddStockPriceDownFluctuation(float value)
        {
            StockPriceDownFluctuation += value;
            StockPriceDownFluctuation = StockPriceDownFluctuation < 0 ? 0 : StockPriceDownFluctuation;
        }
        
        public void AddStockPriceUpFluctuation(float value)
        {
            StockPriceUpFluctuation += value;
            StockPriceUpFluctuation = StockPriceUpFluctuation < 0 ? 0 : StockPriceUpFluctuation;
        }
    }
}
