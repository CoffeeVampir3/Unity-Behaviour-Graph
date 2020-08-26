using System;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Composite;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.CompositeNodes
{
    [Serializable]
    public class SelectorNode : CompositeNode
    {
        [NonSerialized, OdinSerialize]
        [HideInInspector]
        protected TreeSelectorNode selectorNode;
        
        protected override void OnCreation()
        {
            selectorNode = new TreeSelectorNode(null);
            thisTreeNode = selectorNode;
        }
        
        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree)
        {
            var node = selectorNode;
            node.parentTree = tree;
            
            WalkCompositeNodeChildren(node, tree);
            return node;
        }
    }
}