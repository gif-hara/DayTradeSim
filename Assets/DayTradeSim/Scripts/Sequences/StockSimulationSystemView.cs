using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnitySequencerSystem;

namespace DayTradeSim
{
    [Serializable]
    public class StockSimulationSystemView : ISequence
    {
        public UniTask PlayAsync(Container container, CancellationToken cancellationToken)
        {
            var simulator = container.Resolve<StockSimulator.Core>();
            Debug.Log($"Money: {simulator.Money}, Principal: {simulator.Principal}, Portfolio: {simulator.Portfolio}, Rate: {simulator.PortfolioRate}%");
            foreach (var i in simulator.Companies)
            {
                Debug.Log($"{i.Id:0000} {i.Name}: {i.StockPrice:0.00}");
            }
            foreach (var i in simulator.BuyList)
            {
                var company = simulator.GetCompany(i.Key);
                Debug.Log($"Buy: {company.Name} {i.Value}");
            }
            return UniTask.CompletedTask;
        }
    }
}
