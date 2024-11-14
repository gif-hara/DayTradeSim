using System;
using System.Collections.Generic;
using UnityEngine;

namespace DayTradeSim.StockSimulator
{
    [CreateAssetMenu(menuName = "DayTradeSim/NewsGeneratorDefault")]
    public class NewsGeneratorDefault : NewsGenerator
    {
        [SerializeField]
        private List<Element> elements;
        
        public override News Generate(Company company)
        {
            var element = elements[UnityEngine.Random.Range(0, elements.Count)];
            return new News(company.Id, element.Title, element.Content);
        }

        [Serializable]
        public class Element
        {
            [SerializeField]
            private string title;
            public string Title => title;
            
            [SerializeField]
            private string content;
            public string Content => content;
        }
    }
}
