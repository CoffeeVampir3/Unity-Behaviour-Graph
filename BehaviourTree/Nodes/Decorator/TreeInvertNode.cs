using Coffee.BehaviourTree.Ctx;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeInvertNode : TreeDecoratorNode
    {
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