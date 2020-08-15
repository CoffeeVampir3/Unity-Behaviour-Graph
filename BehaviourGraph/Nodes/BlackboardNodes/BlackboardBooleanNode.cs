namespace Coffee.Behaviour.Nodes.BlackboardNodes
{
    public class BlackboardBooleanNode : BlackboardBaseNode
    {
        [Output(ShowBackingValue.Always, ConnectionType.Multiple, TypeConstraint.Strict)]
        public bool outputValue;
    }
}