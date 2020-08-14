using Coffee.Behaviour.Nodes.Private;

namespace Coffee.Behaviour.Nodes.DecoratorNodes
{
    public abstract class DecoratorNode : RootDecoratorNode
    {
        [InputAttribute(ShowBackingValue.Never)] public BaseNode[] parents;
    }
}