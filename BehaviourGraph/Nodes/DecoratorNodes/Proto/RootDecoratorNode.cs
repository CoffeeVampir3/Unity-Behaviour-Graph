using System;
using Coffee.BehaviourTree.Decorator;
using Sirenix.Serialization;
using UnityEngine;
using XNode;

namespace Coffee.Behaviour.Nodes.Private
{
    [Serializable]
    internal abstract class RootDecoratorNode : BaseNode
    {
        [SerializeField]
        [Output(ShowBackingValue.Never, ConnectionType.Override)] public BaseNode childNode;
        
        protected TreeDecoratorNode WalkDecoratorNode(BehaviourTree.BehaviourTree tree, TreeDecoratorNode node)
        {
            node.parentTree = tree;
            var p = GetOutputPort("childNode");

            BaseNode b = p.Connection.node as BaseNode;
            if (b == null)
            {
                Debug.LogError("Behaviour graph node: " + this.name + " was not connected to a child.", this);
            }

            node.child = b.WalkGraphToCreateTree(tree);
            return node;
        }
        
        protected void SetDecoratorNodeChild(BaseNode incomingNode)
        {
            TreeDecoratorNode decNode = thisTreeNode as TreeDecoratorNode;
            UnityEngine.Debug.Assert(
                decNode != null, nameof(decNode) + " != null");

            decNode.child = incomingNode.thisTreeNode;
        }

        protected void RemoveDecoratorNodeChild()
        {
            childNode = null;
            
            TreeDecoratorNode decNode = thisTreeNode as TreeDecoratorNode;
            UnityEngine.Debug.Assert(
                decNode != null, nameof(decNode) + " != null");
            
            decNode.child = null;
        }
        
        public override void OnCreateConnection(NodePort @from, NodePort to)
        {
            if (from.node == this && from.IsOutput)
            {
                SetDecoratorNodeChild(to.node as BaseNode);
            }
        }

        public override void OnRemoveConnection(NodePort port)
        {
            if (!port.IsOutput)
                return;
            
            RemoveDecoratorNodeChild();
        }
    }
}