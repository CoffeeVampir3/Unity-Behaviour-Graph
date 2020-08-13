namespace Coffee.BehaviourTree.Decorator
{
    public abstract class DecoratorNode : BaseNode
    {
        private readonly IBehaviourNode child;
        public IBehaviourNode Child => child;

        public DecoratorNode(BehaviourTree tree, IBehaviourNode child) : base(tree)
        {
            this.child = child;
        }
    }
}