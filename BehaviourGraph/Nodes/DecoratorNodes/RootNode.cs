using System;
using Coffee.BehaviourTree.Decorator;

namespace Coffee.Behaviour.Nodes.Private
{
    [Serializable]
    public class RootNode : RootDecoratorNode
    {
        protected override void OnCreation()
        {
            thisTreeNode = new TreeRootNode(parentTree);
        }
    }
}