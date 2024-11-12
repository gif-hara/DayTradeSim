using System.Collections.Generic;
using UnityEngine;
using UnitySequencerSystem;

namespace DayTradeSim
{
    [CreateAssetMenu(fileName = "CommandData", menuName = "DayTradeSim/CommandData", order = 0)]
    public class CommandData : ScriptableObject
    {
        [SerializeField]
        private string commandName;
        public string CommandName => commandName;

        [SubclassSelector]
        [SerializeReference]
        private List<ISequence> sequences;
        public List<ISequence> Sequences => sequences;
    }
}
