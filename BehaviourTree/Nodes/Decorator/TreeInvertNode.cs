using Coffee.BehaviourTree.Context;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeInvertNode : TreeDecoratorNode
    {
        public override Result Execute(ref BehaviourContext context)
        {
            var result = child.Execute(ref context);
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
        
        public TreeInvertNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}