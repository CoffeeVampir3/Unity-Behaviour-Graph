using Coffee.BehaviourTree.Decorator;

namespace Coffee.Behaviour.Nodes.DecoratorNodes
{
    public class RepeaterNode : DecoratorNode
    {
        protected override void Init()
        {
            base.Init();
            thisTreeNode = new TreeRepeaterNode(parentTree);
            parentTree.nodes.Add(thisTreeNode);
        }
    }
}