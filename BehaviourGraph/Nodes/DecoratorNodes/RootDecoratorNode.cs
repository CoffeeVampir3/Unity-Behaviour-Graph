namespace Coffee.Behaviour.Nodes.Private
{
    public abstract class RootDecoratorNode : BaseNode
    {
        [Output(ShowBackingValue.Never, ConnectionType.Override)] public BaseNode childNode;
    }
}