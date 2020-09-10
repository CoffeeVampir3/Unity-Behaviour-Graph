using BehaviourGraph.Blackboard;
using Coffee.BehaviourTree.Context;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeConditionNode : TreeDecoratorNode
    {
        public BlackboardRuntimeCondition runtimeCondition;
        public TreeConditionNode(BehaviourTree tree) : base(tree)
        {
        }

        public override Result Execute(ref BehaviourContext context)
        {
            Debug.Assert(runtimeCondition != null);

            if (runtimeCondition.Evaluate())
                return child.Execute(ref context);
            
            return Result.Failure;
        }

        public override void Reset()
        {
            child?.Reset();
        }
    }
}