using Coffee.BehaviourTree.Decorator;

namespace Coffee.Behaviour.Nodes.Private
{
    public class RootNode : RootDecoratorNode
    {
        protected override void Init()
        {
            base.Init();
            thisTreeNode = new TreeRootNode(parentTree);
            parentTree.nodes.Add(thisTreeNode);
        }
    }
}