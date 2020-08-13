namespace Coffee.BehaviourTree
{
    public abstract class BaseNode : IBehaviourNode
    {
        private BehaviourTree parentTree;
        
        public enum Result
        {
            Failure,
            Success,
            Running
        }
        
        public BaseNode(BehaviourTree tree)
        {
            parentTree = tree;
        }

        public abstract Result Execute();
    }
}