using Coffee.BehaviourTree.Decorator;

namespace Coffee.Behaviour.Nodes.Private
{
    public class RootNode : RootDecoratorNode
    {
        protected override void Init()
        {
            base.Init();
            thisNode = new TreeRootNode(parentTree);
        }
    }
}