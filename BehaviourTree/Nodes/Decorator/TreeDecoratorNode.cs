namespace Coffee.BehaviourTree.Decorator
{
    public abstract class TreeDecoratorNode : TreeBaseNode
    {
        public ITreeBehaviourNode child;

        protected TreeDecoratorNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}