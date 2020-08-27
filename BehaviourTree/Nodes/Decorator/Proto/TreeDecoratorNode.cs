namespace Coffee.BehaviourTree.Decorator
{
    internal abstract class TreeDecoratorNode : TreeBaseNode
    {
        public TreeBaseNode child;

        protected TreeDecoratorNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}