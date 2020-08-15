using Coffee.BehaviourTree.Decorator;

namespace Coffee.Behaviour.Nodes.DecoratorNodes
{
    public class RepeaterNode : DecoratorNode
    {
        protected override void OnCreation()
        {
            thisTreeNode = new TreeRepeaterNode(parentTree);
            parentTree.nodes.Add(thisTreeNode);
        }
    }
}