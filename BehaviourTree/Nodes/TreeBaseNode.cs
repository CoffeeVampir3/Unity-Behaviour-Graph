namespace Coffee.BehaviourTree
{
    public abstract class TreeBaseNode : ITreeBehaviourNode
    {
        private BehaviourTree parentTree;
        
        public enum Result
        {
            Failure,
            Success,
            Running
        }
        
        protected TreeBaseNode(BehaviourTree tree)
        {
            parentTree = tree;
        }

        public abstract Result Execute();
    }
}