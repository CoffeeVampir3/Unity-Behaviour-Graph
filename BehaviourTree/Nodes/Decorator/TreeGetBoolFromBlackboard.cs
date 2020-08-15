namespace Coffee.BehaviourTree.Decorator
{
    public class TreeGetBoolFromBlackboard : TreeBaseNode
    {
        protected int blackboardIndex = -1;
        
        public TreeGetBoolFromBlackboard(BehaviourTree tree) : base(tree)
        {
        }

        public override Result Execute()
        {
            bool b = parentTree.Blackboard.GetItem<bool>(blackboardIndex);
            if (b)
                return Result.Success;

            return Result.Failure;
        }

        public override void Reset()
        {
            
        }
    }
}