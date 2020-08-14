namespace Coffee.BehaviourTree.Decorator
{
    public abstract class DecoratorNode : BaseNode
    {
        public IBehaviourNode child;

        public DecoratorNode(BehaviourTree tree, IBehaviourNode child) : base(tree)
        {
            this.child = child;
        }
    }
}