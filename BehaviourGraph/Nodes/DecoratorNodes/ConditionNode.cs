using System;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Decorator;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.DecoratorNodes
{
    [Serializable]
    public partial class ConditionNode : DecoratorNode
    {
        [NonSerialized, OdinSerialize]
        protected TreeConditionNode conditionNode;

        protected override void OnCreation()
        {
            conditionNode = new TreeConditionNode(null);
            thisTreeNode = conditionNode;
            conditionNode.reference = blackboardReferenceTarget;
        }
        
        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree)
        {
            var node = conditionNode;
            node.parentTree = tree;
            node.reference = blackboardReferenceTarget;
            var p = GetOutputPort("childNode");

            BaseNode b = p.Connection.node as BaseNode;
            if (b == null)
            {
                Debug.LogError("Behaviour graph node: " + this.name + " was not connected to a child.", this);
                throw new NullReferenceException("Behaviour graph could not build into a valid tree due to null children.");
            }

            node.child = b.WalkGraphToCreateTree(tree);
            return node;
        }
    }
}