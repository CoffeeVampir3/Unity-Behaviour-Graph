using System;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Decorator;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.DecoratorNodes
{
    [Serializable]
    internal partial class ConditionNode : DecoratorNode
    {
        [NonSerialized, OdinSerialize, HideInInspector]
        protected TreeConditionNode conditionNode;

        protected override void OnCreation()
        {
            conditionNode = new TreeConditionNode(null);
            thisTreeNode = conditionNode;
        }
        
        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree)
        {
            TreeConditionNode node = conditionNode;
            node.parentTree = tree;
            node.reference = blackboardReferenceTarget;
            return WalkDecoratorNode(tree, node);
        }
    }
}