using System;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Leaf;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace Coffee.Behaviour.Nodes.LeafNodes
{
    internal class ServiceLeafNode : LeafNode
    {
        [SerializeField] 
        public UnityEvent service;
        [NonSerialized, OdinSerialize, HideInInspector]
        protected  TreeServiceLeafNode leafNode;
        
        protected override void OnCreation()
        {
            leafNode = new TreeServiceLeafNode(null);
            thisTreeNode = leafNode;
        }

        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree)
        {
            var node = leafNode;
            leafNode.serviceEvent = service;
            node.parentTree = tree;
            return node;
        }
    }
}