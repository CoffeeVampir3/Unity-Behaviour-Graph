using Coffee.BehaviourTree.Ctx;

namespace Coffee.BehaviourTree.Decorator
{
    /// <summary>
    /// Decorator nodes mutate their child return values.
    /// </summary>
    internal abstract class TreeDecoratorNode : TreeBaseNode
    {
        public TreeBaseNode child;

        protected TreeDecoratorNode(BehaviourTree tree, Context parentCtx) : 
            base(tree, parentCtx)
        {
            //(base)
        }
    }
}