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
            foreach (var i in simulator.Companies)
            {
                Debug.Log($"{i.Id:0000} {i.Name} : {i.StockPrice}");
            }
            return UniTask.CompletedTask;
        }
    }
}