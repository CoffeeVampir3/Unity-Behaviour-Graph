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
        [SerializeField]
        [InputAttribute(ShowBackingValue.Never)] public BaseNode[] parents = null;
        [SerializeField]
        [Output] public BaseNode[] children = null;

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

        protected void WalkCompositeNodeChildren(TreeCompositeNode composite, BehaviourTree.BehaviourTree tree)
        {
            var childrenPort = GetOutputPort("children");
            var connections = childrenPort.GetConnections();
            List<TreeBaseNode> treeNodes = new List<TreeBaseNode>();

            foreach (var connector in connections)
            {
                BaseNode bn = connector.node as BaseNode;
                if (bn == null)
                {
                    Debug.LogError("Behaviour graph node: " + this.name + " was not connected to a child.", this);
                    throw new NullReferenceException("Behaviour graph could not build into a valid tree due to null children.");
                }
                
                treeNodes.Add(bn.WalkGraphToCreateTree(tree));
            }
            
            composite.SetChildren(treeNodes);
        }
    }
}