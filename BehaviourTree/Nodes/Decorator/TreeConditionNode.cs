using System;
using BehaviourGraph.Blackboard;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Coffee.BehaviourTree.Decorator
{
    [Serializable]
    [ShowOdinSerializedPropertiesInInspector]
    internal class TreeConditionNode : TreeDecoratorNode
    {
        [NonSerialized, OdinSerialize]
        public BlackboardReference reference;
        public TreeConditionNode(BehaviourTree tree) : base(tree)
        {
        }

        public override Result Execute()
        {
            if (reference == null)
            {
                return Result.Failure;
            }

            if (reference.Evaluate())
                return child.Execute();
            
            return Result.Failure;
        }

        public override void Reset()
        {
            child?.Reset();
        }
    }
}