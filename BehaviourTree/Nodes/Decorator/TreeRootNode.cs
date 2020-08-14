namespace Coffee.BehaviourTree.Decorator
{
    public class TreeRootNode : TreeDecoratorNode
    {
        public override Result Execute()
        {
            return child.Execute();
        }

        public TreeRootNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}