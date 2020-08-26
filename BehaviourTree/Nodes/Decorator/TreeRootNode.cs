using System;

namespace Coffee.BehaviourTree.Decorator
{
    [Serializable]
    public class TreeRootNode : TreeDecoratorNode
    {
        public override Result Execute()
        {
            if (child == null)
            {
                return Result.Failure;
            }

            return child.Execute();
        }

        public override void Reset()
        {
            child?.Reset();
        }

        public TreeRootNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}