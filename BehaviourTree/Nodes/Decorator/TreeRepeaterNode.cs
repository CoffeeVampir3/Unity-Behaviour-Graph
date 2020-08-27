namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeRepeaterNode : TreeDecoratorNode
    {
        public override Result Execute()
        {
            child?.Execute();
            return Result.Running;
        }

        public override void Reset()
        {
            child?.Reset();
        }

        public TreeRepeaterNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}