using Coffee.BehaviourTree.Context;

namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeRootNode : TreeDecoratorNode
    {
        public override Result Execute(ref BehaviourContext context)
        {
            if (child == null)
            {
                return Result.Failure;
            }

            return child.Execute(ref context);
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