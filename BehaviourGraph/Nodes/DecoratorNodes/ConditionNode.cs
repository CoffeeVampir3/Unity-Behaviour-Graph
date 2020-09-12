using System;
using BehaviourGraph.Blackboard;
using BehaviourGraph.CodeLinks.AttributeCache;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Ctx;
using Coffee.BehaviourTree.Decorator;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.DecoratorNodes
{
    [Serializable]
    internal class ConditionNode : DecoratorNode
    {
        [NonSerialized, OdinSerialize, HideInInspector]
        protected TreeConditionNode conditionNode;

        [NonSerialized, OdinSerialize] 
        private ConditionalSelector conditionalSelector = new ConditionalSelector();

        protected override void OnCreation()
        {
            conditionNode = new TreeConditionNode(null, null);
            thisTreeNode = conditionNode;
        }
        
        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree, Context currentContext)
        {
            RuntimeCondition brtc = 
                new RuntimeCondition(conditionalSelector, tree.owner);
            
            TreeConditionNode node = new TreeConditionNode(tree, currentContext) 
                {runtimeCondition = brtc};
            
            return WalkDecoratorNode(tree, node);
        }
    }
}