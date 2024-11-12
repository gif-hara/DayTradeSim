using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnitySequencerSystem;

namespace DayTradeSim
{
    [Serializable]
    public class ViewStockSimulationSystem : ISequence
    {
        public UniTask PlayAsync(Container container, CancellationToken cancellationToken)
        {
            Debug.Log("SSS");
            return UniTask.CompletedTask;
        }
    }
}
