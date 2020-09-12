using Coffee.BehaviourTree.Ctx;

namespace Coffee.BehaviourTree.Decorator
{
    internal abstract class TreeDecoratorNode : TreeBaseNode
    {
        public TreeBaseNode child;

        public TreeDecoratorNode(BehaviourTree tree, Context parentCtx) : 
            base(tree, parentCtx)
        {
        }
    }
}