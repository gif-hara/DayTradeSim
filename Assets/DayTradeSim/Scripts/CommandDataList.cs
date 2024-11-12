using System;
using HK;
using UnityEngine;

namespace DayTradeSim
{
    [Serializable]
    public class CommandDataList
    {
        [SerializeField]
        private CommandData data;

        [Serializable]
        public class DictionaryList : DictionaryList<string, CommandData>
        {
            public DictionaryList() : base(x => x.CommandName)
            {
            }
        }
    }
}
