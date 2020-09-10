using System;
using BehaviourGraph.Blackboard;
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

        [NonSerialized, OdinSerialize] 
        private ConditionalSelector conditionalSelector = new ConditionalSelector();

        protected override void OnCreation()
        {
            conditionNode = new TreeConditionNode(null);
            thisTreeNode = conditionNode;
        }
        
        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree)
        {
            BlackboardRuntimeCondition brtc = 
                new BlackboardRuntimeCondition(conditionalSelector, tree.owner);
            
            TreeConditionNode node = new TreeConditionNode(tree) 
                {runtimeCondition = brtc};
            
            return WalkDecoratorNode(tree, node);
        }
    }
}