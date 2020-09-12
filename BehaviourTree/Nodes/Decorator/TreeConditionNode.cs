using BehaviourGraph.Blackboard;
using Coffee.BehaviourTree.Ctx;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeConditionNode : TreeDecoratorNode
    {
        public RuntimeCondition runtimeCondition;

        /// <summary>
        /// Evaluates its bound runtime condition.
        /// </summary>
        /// <returns>
        /// If the condition was true returns the child value.
        /// If the condition was false returns failure.
        /// </returns>
        public override Result Execute()
        {
            if (runtimeCondition.Evaluate())
            {
                var isRunning = child.Execute();
                if (isRunning == Result.Running)
                    parentTree.contextWalker.SetContextPointer(context);
                return isRunning;
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