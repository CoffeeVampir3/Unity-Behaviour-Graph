﻿using System;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Decorator;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.Private
{
    [Serializable]
    public class RootNode : RootDecoratorNode
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
            var treeRoot = rootNode;
            treeRoot.parentTree = tree;
            return WalkDecoratorNode(tree, treeRoot);
        }
    }
}