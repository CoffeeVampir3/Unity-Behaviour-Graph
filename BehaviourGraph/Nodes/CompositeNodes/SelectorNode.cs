using System;
using Coffee.BehaviourTree.Composite;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.CompositeNodes
{
    [Serializable]
    public class SelectorNode : CompositeNode
    {
        [SerializeField] 
        [HideInInspector]
        protected TreeSelectorNode selectorNode;
        
        protected override void OnCreation()
        {
            selectorNode = new TreeSelectorNode(parentTree);
            thisTreeNode = selectorNode;
        }
    }
}