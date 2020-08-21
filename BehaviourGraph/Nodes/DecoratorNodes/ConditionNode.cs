using System;
using BehaviourGraph.Blackboard;
using Coffee.BehaviourTree.Decorator;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.DecoratorNodes
{
    [Serializable]
    public class ConditionNode : DecoratorNode
    {
        [OdinSerialize]
        protected BlackboardReference blackboardReferenceTarget;
        [OdinSerialize, HideInInspector]
        protected TreeConditionNode conditionNode;

        protected override void OnCreation()
        {
            conditionNode = new TreeConditionNode(parentTree);
            thisTreeNode = conditionNode;
        }
    }
}