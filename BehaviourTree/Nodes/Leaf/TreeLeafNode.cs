using System;

namespace Coffee.BehaviourTree.Leaf
{
    [Serializable]
    public abstract class TreeLeafNode : TreeBaseNode
    {
        protected TreeLeafNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}