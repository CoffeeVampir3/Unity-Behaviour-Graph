using System;
using System.Collections.Generic;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Composite;
using UnityEngine;
using XNode;

namespace Coffee.Behaviour.Nodes.CompositeNodes
{
    [Serializable]
    public abstract class CompositeNode : BaseNode
    {
        [SerializeField]
        [InputAttribute(ShowBackingValue.Never)] public BaseNode[] parents;
        [SerializeField]
        [Output] public BaseNode[] children;

        protected void BuildChildConnections(NodePort thisPort)
        {
            TreeCompositeNode compositeNode = thisTreeNode as TreeCompositeNode;
            Debug.Assert(compositeNode != null, nameof(compositeNode) + " != null");

            var connections = thisPort.GetConnections();
            List<TreeBaseNode> treeNodes = new List<TreeBaseNode>();

            foreach (var connector in connections)
            {
                treeNodes.Add(((BaseNode)connector.node).thisTreeNode);
            }
            
            compositeNode.SetChildren(treeNodes);
        }
        
        public override void OnCreateConnection(NodePort @from, NodePort to)
        {
            if (from.node == this && from.IsOutput)
            {
                BuildChildConnections(from);
            }
        }

        public override void OnRemoveConnection(NodePort port)
        {
            if (!port.IsOutput)
                return;
            
            BuildChildConnections(port);
        }
    }
}