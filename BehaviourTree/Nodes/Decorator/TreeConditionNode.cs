using BehaviourGraph.Blackboard;
using Coffee.BehaviourTree.Context;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeConditionNode : TreeDecoratorNode
    {
        public BlackboardReference reference;
        public TreeConditionNode(BehaviourTree tree) : base(tree)
        {
        }

        public override Result Execute(ref BehaviourContext context)
        {
            Debug.Assert(reference != null);

            if (reference.Evaluate())
                return child.Execute(ref context);
            
            return Result.Failure;
        }

        public override void Reset()
        {
            child?.Reset();
        }
    }
}