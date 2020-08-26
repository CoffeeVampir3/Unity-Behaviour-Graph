using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Coffee.BehaviourTree.Decorator
{
    [Serializable]
    [ShowOdinSerializedPropertiesInInspector]
    public abstract class TreeDecoratorNode : TreeBaseNode
    {
        [NonSerialized, OdinSerialize]
        //[HideInInspector]
        public TreeBaseNode child;

        protected TreeDecoratorNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}