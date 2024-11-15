using UnityEngine;

namespace DayTradeSim.StockSimulator
{
    public abstract class NewsGenerator : ScriptableObject, INewsGenerator
    {
        public abstract INews Generate(Core core);
    }
}
