using System;
using System.Collections.Generic;
using UnityEngine;

namespace Coffee.BehaviourTree.Composite
{
    [Serializable]
    public abstract class TreeCompositeNode : TreeBaseNode
    {
        [SerializeField]
        public TreeBaseNode[] childNodes;

        public void SetChildren(List<TreeBaseNode> nodes)
        {
            childNodes = nodes.ToArray();
        }

        protected TreeCompositeNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}