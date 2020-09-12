using Coffee.BehaviourTree.Ctx;

namespace Coffee.BehaviourTree.Leaf
{
    internal abstract class TreeLeafNode : TreeBaseNode
    {
        protected TreeLeafNode(BehaviourTree tree, Context parentCtx) : 
            base(tree, parentCtx)
        {
        }
    }
}