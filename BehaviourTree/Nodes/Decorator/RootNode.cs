namespace Coffee.BehaviourTree.Decorator
{
    public class RootNode : DecoratorNode
    {
        public RootNode(BehaviourTree tree, IBehaviourNode child) : base(tree, child)
        {
        }

        public override Result Execute()
        {
            return child.Execute();
        }
    }
}