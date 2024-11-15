using System;
using System.Linq;
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
            var commandLine = container.Resolve<CommandLine>();
            var sb = new StringBuilder();
            if(commandLine.FindArgumentToInt("-c", out var companyId))
            {
                var company = simulator.GetCompany(companyId);
                sb.AppendLine($"Company: [{companyId}] {company.Name} {company.StockPrice:0.00}");
                sb.AppendLine("Categories");
                foreach (var i in company.Categories)
                {
                    sb.AppendLine($"    {i}");
                }
            }
            else
            {
                sb.AppendLine($"Money: {simulator.Money}, Principal: {simulator.Principal}, Portfolio: {simulator.Portfolio}, Rate: {simulator.PortfolioRate}%");
                sb.AppendLine("Companies");
                foreach (var i in simulator.Companies)
                {
                    var categories = string.Join(", ", i.Categories.Select(x => x.ToString()));
                    sb.AppendLine($"    {i.Id:0000} {i.Name}: {i.StockPrice:0.00} [{categories}]");
                }
                sb.AppendLine("BuyList");
                foreach (var i in simulator.BuyList)
                {
                    var company = simulator.GetCompany(i.Key);
                    sb.AppendLine($"    {company.Name} {i.Value}");
                }

                sb.AppendLine("News");
                foreach (var i in simulator.News)
                {
                    sb.AppendLine($"    {i.Title}");
                    sb.AppendLine($"    {i.Content}");
                    sb.AppendLine("-----");
                }
            }
            Debug.Log(sb.ToString());
            return UniTask.CompletedTask;
        }
    }
}
