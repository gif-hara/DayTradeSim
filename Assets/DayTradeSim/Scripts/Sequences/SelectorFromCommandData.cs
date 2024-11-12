using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnitySequencerSystem;
using UnitySequencerSystem.Resolvers;

namespace DayTradeSim
{
    [Serializable]
    public class SelectorFromCommandData : ISequence
    {
        [SerializeReference, SubclassSelector]
        private IntResolver indexResolver;
        
        [SerializeField]
        private ScriptableSequences defaultSequences;
        
        [SerializeField]
        private List<Element> elements;
        
        public async UniTask PlayAsync(Container container, CancellationToken cancellationToken)
        {
            var index = indexResolver.Resolve(container);
            var data = container.Resolve<List<string>>("Data");
            if(data.Count <= index)
            {
                await new Sequencer(container, defaultSequences.Sequences).PlayAsync(cancellationToken);
                return;
            }

            var target = data[index];
            foreach (var i in elements)
            {
                if (i.TargetResolver.Resolve(container) == target)
                {
                    var sequencer = new Sequencer(container, i.Sequences.Sequences);
                    await sequencer.PlayAsync(cancellationToken);
                    return;
                }
            }
            var defaultSequencer = new Sequencer(container, defaultSequences.Sequences);
            await defaultSequencer.PlayAsync(cancellationToken);
        }

        [Serializable]
        public class Element
        {
            [SerializeReference, SubclassSelector]
            private StringResolver targetResolver;
            public StringResolver TargetResolver => targetResolver;
            
            [SerializeField]
            private ScriptableSequences sequences;
            public ScriptableSequences Sequences => sequences;
        }
    }
}
