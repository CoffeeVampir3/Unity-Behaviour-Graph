namespace Coffee.Behaviour.Nodes.LeafNodes
{
    public abstract class LeafNode : BaseNode
    {
        [InputAttribute(ShowBackingValue.Never)] public BaseNode[] parents;
    }
}