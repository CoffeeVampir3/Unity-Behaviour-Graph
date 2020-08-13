namespace Coffee.BehaviourTree
{
    public abstract class BaseNode : IBehaviourNode
    {
        private BehaviourTree parentTree;

        public BaseNode(BehaviourTree tree)
        {
            parentTree = tree;
        }

        public abstract BehaviourTree.Result Execute();
    }
}