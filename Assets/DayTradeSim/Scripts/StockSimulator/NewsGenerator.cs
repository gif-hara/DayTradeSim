using System;
using System.Collections.Generic;
using UnityEngine;

namespace DayTradeSim.StockSimulator
{
    public abstract class NewsGenerator : ScriptableObject, INewsGenerator
    {
        public abstract INews Generate(Core core);

        [Serializable]
        public class Random
        {
            [Serializable]
            public class NewsCompanies : INewsGenerator
            {
                [SerializeField]
                private string titleFormat;
                
                [SerializeField]
                private string contentFormat;

                [SerializeField]
                private int companyCount;
                
                [SerializeReference, SubclassSelector]
                private List<INewsCompaniesImpact> impacts;
                
                public INews Generate(Core core)
                {
                    var title = titleFormat;
                    var content = contentFormat;
                    var companyIds = new List<int>();
                    for (var i = 0; i < companyCount; i++)
                    {
                        var company = core.Companies[UnityEngine.Random.Range(0, core.Companies.Count)];
                        companyIds.Add(company.Id);
                        title = title.Replace($"{{Company[{i}]}}", company.Name);
                        content = content.Replace($"{{Company[{i}]}}", company.Name);
                    }
                    return new StockSimulator.NewsCompanies(title, content, companyIds, impacts);
                }
            }
        }
    }
}
