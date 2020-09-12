using BehaviourGraph.Blackboard;
using Coffee.BehaviourTree.Ctx;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeConditionNode : TreeDecoratorNode
    {
        public RuntimeCondition runtimeCondition;

        //TODO:: Could be optimized with an alternate jump path.
        public override Result Execute()
        {
            if (runtimeCondition.Evaluate())
            {
                var isRunning = child.Execute();
                if (isRunning == Result.Running)
                    parentTree.contextWalker.SetContextPointer(context);
                return child.Execute();
            }

            return Result.Failure;
        }

        public override void Reset()
        {
            Debug.Assert(child != null);
            Debug.Assert(runtimeCondition != null);
            child.Reset();
        }

        public TreeConditionNode(BehaviourTree tree, Context parentCtx) : 
            base(tree, parentCtx)
        {
            context = new Context(parentCtx, this);
        }
    }
}