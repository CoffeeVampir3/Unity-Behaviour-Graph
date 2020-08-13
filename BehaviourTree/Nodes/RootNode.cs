namespace Coffee.BehaviourTree
{
    public class RootNode : BaseNode
    {
        public RootNode(BehaviourTree tree) : base(tree)
        {
        }

        public override BehaviourTree.Result Execute()
        {
            return BehaviourTree.Result.Failure;
        }
    }
}