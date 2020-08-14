namespace Coffee.BehaviourTree.Composite
{
    public abstract class TreeCompositeNode : TreeBaseNode
    {
        public ITreeBehaviourNode[] childNodes;

        protected TreeCompositeNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}