using System;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    [Serializable]
    public abstract class TreeDecoratorNode : TreeBaseNode
    {
        [SerializeField]
        [HideInInspector]
        public TreeBaseNode child;

        protected TreeDecoratorNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}