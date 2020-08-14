using Coffee.BehaviourTree.Decorator;

namespace Coffee.Behaviour.Nodes.DecoratorNodes
{
    public class RepeaterNode : DecoratorNode
    {
        protected override void Init()
        {
            base.Init();
            thisNode = new TreeRepeaterNode(parentTree);
        }
    }
}