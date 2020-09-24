using System;
using System.Collections.Generic;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Composite;
using Sirenix.Serialization;
using UnityEngine;
using XNode;

namespace Coffee.Behaviour.Nodes.CompositeNodes
{
    [Serializable]
    internal abstract class CompositeNode : BaseNode
    {
        [InputAttribute(ShowBackingValue.Never)] 
        public BaseNode[] parents = null;
        
        [Output(dynamicPortList = true, backingValue = ShowBackingValue.Never)]
        public mew[] children = null;

        public enum mew
        {
            Port
        }

        #region Walk tree Impl
        
        protected void WalkCompositeNodeChildren(TreeCompositeNode composite, BehaviourTree.BehaviourTree tree)
        {
            var childrenPort = GetOutputPort("children");
            var connections = childrenPort.GetConnections();
            List<TreeBaseNode> treeNodes = new List<TreeBaseNode>();

            foreach (var connector in connections)
            {
                BaseNode bn = connector.node as BaseNode;
                Debug.Assert(bn != null, nameof(bn) + " != null");
                if (bn == null)
                {
                    Debug.LogError("Behaviour graph node: " + this.name + " was not connected to a child.", this);
                }
                
                treeNodes.Add(bn.WalkGraphToCreateTree(tree, composite.context));
            }
            
            composite.SetChildren(treeNodes);
        }
        
        #endregion
        
        #region XNode
        
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
        
        #endregion
    }
}