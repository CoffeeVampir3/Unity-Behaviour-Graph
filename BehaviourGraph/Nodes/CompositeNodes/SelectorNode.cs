using System;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Composite;
using Coffee.BehaviourTree.Ctx;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.CompositeNodes
{
    [Serializable]
    internal class SelectorNode : CompositeNode
    {
        [NonSerialized, OdinSerialize]
        [HideInInspector]
        protected TreeSelectorNode selectorNode;
        
        protected override void OnCreation()
        {
            selectorNode = new TreeSelectorNode(null, null);
            thisTreeNode = selectorNode;
        }
        
        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree, Context currentContext)
        {
            var node = new TreeSelectorNode(tree, currentContext);
            WalkCompositeNodeChildren(node, tree);
            return node;
        }
    }
}