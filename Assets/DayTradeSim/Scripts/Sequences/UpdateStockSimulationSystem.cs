using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnitySequencerSystem;

namespace DayTradeSim
{
    [Serializable]
    public class UpdateStockSimulationSystem : ISequence
    {
        public UniTask PlayAsync(Container container, CancellationToken cancellationToken)
        {
            var simulator = container.Resolve<StockSimulator.Core>();
            simulator.Update();
            return UniTask.CompletedTask;
        }
    }
}
