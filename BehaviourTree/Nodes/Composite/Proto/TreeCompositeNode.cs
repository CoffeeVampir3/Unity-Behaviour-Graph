using System.Collections.Generic;

namespace Coffee.BehaviourTree.Composite
{
    internal abstract class TreeCompositeNode : TreeBaseNode
    {
        public TreeBaseNode[] childNodes;
        protected int currentNode = 0;

        public void SetChildren(List<TreeBaseNode> nodes)
        {
            childNodes = nodes.ToArray();
        }

        protected TreeCompositeNode(BehaviourTree tree) : base(tree)
        {
        }

        public override void Reset()
        {
            currentNode = 0;
            for (int i = childNodes.Length - 1; i >= 0; i--)
            {
                childNodes[i].Reset();
            }
        }
    }
}