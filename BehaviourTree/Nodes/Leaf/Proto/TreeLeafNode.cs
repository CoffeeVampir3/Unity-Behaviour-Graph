using Coffee.BehaviourTree.Ctx;

namespace Coffee.BehaviourTree.Leaf
{
    internal abstract class TreeLeafNode : TreeBaseNode
    { 
        public TreeLeafNode(BehaviourTree tree, Context parentCtx) : 
            base(tree, parentCtx)
        {
        }
    }
}