using System;
using System.Threading;
using Cysharp.Threading.Tasks;
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
            return UniTask.CompletedTask;
        }
    }
}
