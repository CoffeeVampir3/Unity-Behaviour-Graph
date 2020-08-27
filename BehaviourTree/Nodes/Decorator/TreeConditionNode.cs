using BehaviourGraph.Blackboard;

namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeConditionNode : TreeDecoratorNode
    {
        public BlackboardReference reference;
        public TreeConditionNode(BehaviourTree tree) : base(tree)
        {
        }

        public override Result Execute()
        {
            if (reference == null)
            {
                return Result.Failure;
            }

            if (reference.Evaluate())
                return child.Execute();
            
            return Result.Failure;
        }

        public override void Reset()
        {
            child?.Reset();
        }
    }
}