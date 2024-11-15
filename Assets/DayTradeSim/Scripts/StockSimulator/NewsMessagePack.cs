using System;
using System.Collections.Generic;
using UnityEngine;

namespace DayTradeSim
{
    [CreateAssetMenu(menuName = "DayTradeSim/NewsMessagePack")]
    public class NewsMessagePack : ScriptableObject
    {
        [SerializeField]
        private List<Element> elements;
        
        public Element GetRandomElement()
        {
            return elements[UnityEngine.Random.Range(0, elements.Count)];
        }
        
        [Serializable]
        public class Element
        {
            [SerializeField]
            private string titleFormat;
            public string TitleFormat => titleFormat;
            
            [SerializeField]
            private string contentFormat;
            public string ContentFormat => contentFormat;
        }
    }
}
