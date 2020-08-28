﻿using System;
using System.Reflection;
using BehaviourGraph.Services;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Leaf;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.LeafNodes
{
    internal class ServiceLeafNode : LeafNode
    {
        [OdinSerialize]
        [ValueDropdown("GetServices", NumberOfItemsBeforeEnablingSearch = 2)]
        public MethodInfo targetMethod = null;
        public ValueDropdownList<MethodInfo> GetServices => ServiceCache.GetListOfServices();
        
        [NonSerialized, OdinSerialize, HideInInspector]
        protected  TreeServiceLeafNode leafNode;
        
        protected override void OnCreation()
        {
            leafNode = new TreeServiceLeafNode(null);
            thisTreeNode = leafNode;
        }

        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree)
        {
            var node = new TreeServiceLeafNode(tree) {targetMethod = targetMethod};
            return node;
        }
    }
}