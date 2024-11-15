using System.Collections.Generic;
using UnityEngine;

namespace DayTradeSim.StockSimulator
{
    [CreateAssetMenu(menuName = "DayTradeSim/NewsGeneratorDefault")]
    public class NewsGeneratorDefault : NewsGenerator
    {
        [SerializeReference, SubclassSelector]
        private List<INewsGenerator> generators;
        
        public override INews Generate(Core core)
        {
            var generator = generators[UnityEngine.Random.Range(0, generators.Count)];
            return generator.Generate(core);
        }
    }
}
