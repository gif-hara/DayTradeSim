using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DayTradeSim.StockSimulator
{
    public abstract class NewsGenerator : ScriptableObject, INewsGenerator
    {
        public abstract INews Generate(Core core);

        [Serializable]
        public class Random
        {
            [Serializable][AddTypeMenu("Random.NewsCompanies")]
            public class NewsCompanies : INewsGenerator
            {
                [SerializeField]
                private NewsMessagePack commonMessagePack;
                
                [SerializeField]
                private List<MessagePackElement> messagePackElements;
                
                [SerializeField]
                private int companyCount;
                
                [SerializeReference, SubclassSelector]
                private List<INewsImpactCompanies> impacts;
                
                public INews Generate(Core core)
                {
                    var companies = new List<Company>();
                    var companyIds = new List<int>();
                    for (var i = 0; i < companyCount; i++)
                    {
                        var company = core.Companies[UnityEngine.Random.Range(0, core.Companies.Count)];
                        companies.Add(company);
                        companyIds.Add(company.Id);
                    }
                    var packs = new List<NewsMessagePack> { commonMessagePack };
                    foreach (var i in companies)
                    {
                        var findPacks = messagePackElements
                            .FindAll(x => i.Categories.Contains(x.Category))
                            .Select(x => x.MessagePack);
                        packs.AddRange(findPacks);
                    }
                    var packElement = packs[UnityEngine.Random.Range(0, packs.Count)].GetRandomElement();
                    var title = packElement.TitleFormat;
                    var content = packElement.ContentFormat;
                    for (var i = 0; i < companyCount; i++)
                    {
                        var company = companies[i];
                        title = title.Replace($"{{CompanyName[{i}]}}", company.Name);
                        title = title.Replace($"{{CompanyId[{i}]}}", company.Id.ToString());
                        content = content.Replace($"{{CompanyName[{i}]}}", company.Name);
                        content = content.Replace($"{{CompanyId[{i}]}}", company.Id.ToString());
                    }
                    return new StockSimulator.NewsCompanies(title, content, companyIds, impacts);
                }

                [Serializable]
                public class MessagePackElement
                {
                    [SerializeField]
                    private Define.CompanyCategory category;
                    public Define.CompanyCategory Category => category;
                    
                    [SerializeField]
                    private NewsMessagePack messagePack;
                    public NewsMessagePack MessagePack => messagePack;
                }
            }
        }
    }
}
