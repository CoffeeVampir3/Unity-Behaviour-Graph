using System;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Decorator;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.Private
{
    [Serializable]
    internal class RootNode : RootDecoratorNode
    {
        [NonSerialized, OdinSerialize]
        [HideInInspector]
        protected TreeRootNode rootNode;
        
        protected override void OnCreation()
        {
            rootNode = new TreeRootNode(null);
            thisTreeNode = rootNode;
        }

        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree)
        {
            var treeRoot = new TreeRootNode(tree);
            return WalkDecoratorNode(tree, treeRoot);
        }
    }
}