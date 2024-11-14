using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnitySequencerSystem;

namespace DayTradeSim
{
    [Serializable]
    public class StockSimulationSystemUpdate : ISequence
    {
        public UniTask PlayAsync(Container container, CancellationToken cancellationToken)
        {
            var simulator = container.Resolve<StockSimulator.Core>();
            simulator.Update();
            Debug.Log($"Update Money: {simulator.Money}, Principal: {simulator.Principal}, Portfolio: {simulator.Portfolio}, Rate: {simulator.PortfolioRate}%");
            return UniTask.CompletedTask;
        }
    }
}
