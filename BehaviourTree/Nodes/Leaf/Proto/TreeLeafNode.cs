using Coffee.BehaviourTree.Ctx;

namespace Coffee.BehaviourTree.Leaf
{
    /// <summary>
    /// Performs some task or service.
    /// </summary>
    internal abstract class TreeLeafNode : TreeBaseNode
    {
        protected TreeLeafNode(BehaviourTree tree, Context parentCtx) : 
            base(tree, parentCtx)
        {
        }
    }
}