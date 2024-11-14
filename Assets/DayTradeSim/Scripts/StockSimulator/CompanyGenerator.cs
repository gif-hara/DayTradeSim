using UnityEngine;

namespace DayTradeSim.StockSimulator
{
    public abstract class CompanyGenerator : ScriptableObject, ICompanyGenerator
    {
        public abstract Company Generate();
    }
}
