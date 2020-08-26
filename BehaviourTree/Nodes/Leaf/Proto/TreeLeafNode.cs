using System;

namespace Coffee.BehaviourTree.Leaf
{
    [Serializable]
    internal abstract class TreeLeafNode : TreeBaseNode
    {
        protected TreeLeafNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}