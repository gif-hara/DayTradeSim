using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnitySequencerSystem;

namespace DayTradeSim
{
    [Serializable]
    public class StockSimulationSystemBuy : ISequence
    {
        [SerializeField]
        private ScriptableSequences errorSequences;
        
        public async UniTask PlayAsync(Container container, CancellationToken cancellationToken)
        {
            var simulator = container.Resolve<StockSimulator.Core>();
            var commandLine = container.Resolve<CommandLine>();
            if (commandLine.Count < 4)
            {
                await new Sequencer(container, errorSequences.Sequences).PlayAsync(cancellationToken);
                return;
            }
            var companyId = commandLine.GetArgumentToInt(2);
            var quantity = commandLine.GetArgumentToInt(3);
            simulator.Buy(companyId, quantity);
        }
    }
}
