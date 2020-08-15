using System;
using UnityEngine;

namespace Coffee.BehaviourTree.Composite
{
    [Serializable]
    public abstract class TreeCompositeNode : TreeBaseNode
    {
        [SerializeField]
        public TreeBaseNode[] childNodes;

        protected TreeCompositeNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}