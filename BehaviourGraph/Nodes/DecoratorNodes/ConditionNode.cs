using System;
using Coffee.BehaviourTree.Decorator;
using Sirenix.Serialization;

namespace Coffee.Behaviour.Nodes.DecoratorNodes
{
    [Serializable]
    public class ConditionNode : DecoratorNode
    {
        [OdinSerialize]
        protected TreeConditionNode conditionNode;
        protected override void OnCreation()
        {
            conditionNode = new TreeConditionNode(parentTree);
            thisTreeNode = conditionNode;
        }
    }
}