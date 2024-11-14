using System;
using UnityEngine;

namespace DayTradeSim.StockSimulator
{
    [CreateAssetMenu(menuName = "DayTradeSim/CompanyGeneratorDefault")]
    public class CompanyGeneratorDefault : CompanyGenerator
    {
        [SerializeField]
        private int initialId;

        [SerializeField]
        private string[] companyFirstNames;
        
        [SerializeField]
        private string[] companyLastNames;
        
        [SerializeField]
        private Range stockPriceRange;

        [SerializeField]
        private Range stockPriceDownFluctuationRange;
        
        [SerializeField]
        private Range stockPriceUpFluctuationRange;
        
        private int createdCount;

        public override Company Generate()
        {
            var companyCategories = Enum.GetValues(typeof(Define.CompanyCategory));
            var companyCategory = (Define.CompanyCategory)companyCategories.GetValue(UnityEngine.Random.Range(0, companyCategories.Length));
            return new Company(
                initialId + createdCount++,
                $"{companyFirstNames[UnityEngine.Random.Range(0, companyFirstNames.Length)]} {companyLastNames[UnityEngine.Random.Range(0, companyLastNames.Length)]}",
                stockPriceRange.RandomValue,
                new System.Collections.Generic.List<Define.CompanyCategory> { companyCategory },
                stockPriceDownFluctuationRange.RandomValue,
                stockPriceUpFluctuationRange.RandomValue
                );
        }
    }
}
