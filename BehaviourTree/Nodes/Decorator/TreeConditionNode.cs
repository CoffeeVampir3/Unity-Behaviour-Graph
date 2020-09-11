using BehaviourGraph.Blackboard;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeConditionNode : TreeDecoratorNode
    {
        public RuntimeCondition runtimeCondition;

        public override Result Execute()
        {
            if (runtimeCondition.Evaluate())
                return child.Execute();
            
            return Result.Failure;
        }

        public override void Reset()
        {
            Debug.Assert(child != null);
            Debug.Assert(runtimeCondition != null);
            child.Reset();
        }
        
        public TreeConditionNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}