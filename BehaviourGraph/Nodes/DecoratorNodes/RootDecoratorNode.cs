using Coffee.BehaviourTree.Decorator;
using UnityEngine;
using XNode;

namespace Coffee.Behaviour.Nodes.Private
{
    public abstract class RootDecoratorNode : BaseNode
    {
        [SerializeField]
        [Output(ShowBackingValue.Never, ConnectionType.Override)] public BaseNode childNode;
        
        protected void SetDecoratorNodeChild(BaseNode incomingNode)
        {
            TreeDecoratorNode decNode = thisTreeNode as TreeDecoratorNode;
            System.Diagnostics.Debug.Assert(
                decNode != null, nameof(decNode) + " != null");
            Debug.Log("Decorator set child.");
            
            decNode.child = incomingNode.thisTreeNode;
        }

        protected void RemoveDecoratorNodeChild()
        {
            childNode = null;
            
            TreeDecoratorNode decNode = thisTreeNode as TreeDecoratorNode;
            System.Diagnostics.Debug.Assert(
                decNode != null, nameof(decNode) + " != null");
            Debug.Log("Decorator removed child.");
            
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