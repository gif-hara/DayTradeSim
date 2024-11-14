using System.Collections.Generic;

namespace DayTradeSim
{
    public class CommandLine
    {
        private readonly List<string> data;
        
        public CommandLine(List<string> data)
        {
            this.data = data;
        }
        
        public int Count => data.Count;
        
        public string GetCommandName()
        {
            return data[0];
        }
        
        public int GetArgumentToInt(int index)
        {
            return int.Parse(data[index]);
        }
        
        public string GetArgumentToString(int index)
        {
            return data[index];
        }
        
        public float GetArgumentToFloat(int index)
        {
            return float.Parse(data[index]);
        }
        
        public bool FindArgumentToInt(string argument, out int value)
        {
            for (var i = 0; i < data.Count; i++)
            {
                if (data[i] == argument)
                {
                    value = int.Parse(data[i + 1]);
                    return true;
                }
            }
            value = 0;
            return false;
        }
        
        public bool FindArgumentToString(string argument, out string value)
        {
            for (var i = 0; i < data.Count; i++)
            {
                if (data[i] == argument)
                {
                    value = data[i + 1];
                    return true;
                }
            }
            value = null;
            return false;
        }

        public bool FindArgumentToFloat(string argument, out float value)
        {
            for (var i = 0; i < data.Count; i++)
            {
                if (data[i] == argument)
                {
                    value = float.Parse(data[i + 1]);
                    return true;
                }
            }
            value = 0.0f;
            return false;
        }
    }
}
