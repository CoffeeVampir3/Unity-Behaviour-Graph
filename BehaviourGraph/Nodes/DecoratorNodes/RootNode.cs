using Coffee.BehaviourTree.Decorator;

namespace Coffee.Behaviour.Nodes.Private
{
    public class RootNode : RootDecoratorNode
    {
        protected override void OnCreation()
        {
            thisTreeNode = new TreeRootNode(parentTree);
            parentTree.nodes.Add(thisTreeNode);
        }
    }
}