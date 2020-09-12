using System.Collections.Generic;
using Coffee.BehaviourTree.Ctx;

namespace Coffee.BehaviourTree.Composite
{
    internal abstract class TreeCompositeNode : TreeBaseNode
    {
        protected TreeBaseNode[] childNodes;
        protected int currentNode = 0;

        public void SetChildren(List<TreeBaseNode> nodes)
        {
            childNodes = nodes.ToArray();
        }

        public override void Reset()
        {
            currentNode = 0;
            for (int i = childNodes.Length - 1; i >= 0; i--)
            {
                childNodes[i].Reset();
            }
        }
        
        public TreeCompositeNode(BehaviourTree tree, Context parentCtx) : 
            base(tree, parentCtx)
        {
        }
    }
}