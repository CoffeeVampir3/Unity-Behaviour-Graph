using System;
using Coffee.BehaviourTree.Decorator;

namespace Coffee.Behaviour.Nodes.DecoratorNodes
{
    [Serializable]
    public class RepeaterNode : DecoratorNode
    {
        protected override void OnCreation()
        {
            thisTreeNode = new TreeRepeaterNode(parentTree);
        }
    }
}