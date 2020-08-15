using UnityEngine;

namespace Coffee.Behaviour.Nodes.BlackboardNodes
{
    public class BlackboardIndexNode : BlackboardBaseNode
    {
        [Output(ShowBackingValue.Always)]
        [SerializeField]
        public Object outputValue;
        
        [SerializeField]
        private int index = -1;
    }
}