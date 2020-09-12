using Coffee.BehaviourTree.Ctx;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeRepeaterNode : TreeDecoratorNode
    {
        /// <summary>
        /// Hijacks the return of the child and always returns running.
        /// </summary>
        /// <returns>
        /// Always returns running.
        /// </returns>
        public override Result Execute()
        {
            child.Execute();
            return Result.Running;
        }

        public override void Reset()
        {
            Debug.Assert(child != null);
            child.Reset();
        }

        public TreeRepeaterNode(BehaviourTree tree, Context parentCtx) : 
            base(tree, parentCtx)
        {
        }
    }
}