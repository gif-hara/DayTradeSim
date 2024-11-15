using System;
using System.Collections.Generic;
using UnityEngine;

namespace DayTradeSim.StockSimulator
{
    [CreateAssetMenu(menuName = "DayTradeSim/NewsGeneratorDefault")]
    public class NewsGeneratorDefault : NewsGenerator
    {
        [SerializeReference, SubclassSelector]
        private List<INews> elements;
        
        public override INews Generate(Core core)
        {
            var element = elements[UnityEngine.Random.Range(0, elements.Count)];
            throw new NotImplementedException();
        }
    }
}
