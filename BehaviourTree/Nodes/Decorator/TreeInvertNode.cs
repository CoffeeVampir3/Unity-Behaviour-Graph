using Coffee.BehaviourTree.Ctx;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeInvertNode : TreeDecoratorNode
    {
        /// <summary>
        /// Inverts the result of connected nodes.
        /// </summary>
        /// <returns>
        /// Success if failure.
        /// Failure if success.
        /// Running if running.
        /// </returns>
        public override Result Execute()
        {
            var result = child.Execute();
            switch (result)
            {
                case Result.Failure:
                    return Result.Success;
                case Result.Success:
                    return Result.Failure;
            }
            return result;
        }

        public override void Reset()
        {
            Debug.Assert(child != null);
            child.Reset();
        }
        
        public TreeInvertNode(BehaviourTree tree, Context parentCtx) : 
            base(tree, parentCtx)
        {
        }
    }
}