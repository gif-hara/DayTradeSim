using System;

namespace DayTradeSim
{
    [Serializable]
    public sealed class Range
    {
        public float min;
        
        public float max;
        
        public float RandomValue => UnityEngine.Random.Range(min, max);
    }
}
