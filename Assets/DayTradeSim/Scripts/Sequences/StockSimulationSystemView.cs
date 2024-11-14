using System;
using System.Text;
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
            var sb = new StringBuilder();
            sb.AppendLine($"Money: {simulator.Money}, Principal: {simulator.Principal}, Portfolio: {simulator.Portfolio}, Rate: {simulator.PortfolioRate}%");
            sb.AppendLine("Companies");
            foreach (var i in simulator.Companies)
            {
                sb.AppendLine($"    {i.Id:0000} {i.Name}: {i.StockPrice:0.00}");
            }
            sb.AppendLine("BuyList");
            foreach (var i in simulator.BuyList)
            {
                var company = simulator.GetCompany(i.Key);
                sb.AppendLine($"    {company.Name} {i.Value}");
            }
            Debug.Log(sb.ToString());
            return UniTask.CompletedTask;
        }
    }
}
