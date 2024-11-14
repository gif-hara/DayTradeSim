using UnityEngine;

namespace DayTradeSim.StockSimulator
{
    public abstract class NewsGenerator : ScriptableObject, INewsGenerator
    {
        public abstract News Generate(Company company);
    }
}
