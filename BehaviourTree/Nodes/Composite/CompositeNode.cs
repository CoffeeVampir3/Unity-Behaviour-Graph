namespace Coffee.BehaviourTree.Composite
{
    public abstract class CompositeNode : BaseNode
    {
        public IBehaviourNode[] childNodes;
        
        public CompositeNode(BehaviourTree tree, IBehaviourNode[] children) : base(tree)
        {
            childNodes = children;
        }
    }
}