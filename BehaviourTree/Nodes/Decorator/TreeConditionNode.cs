using System;

namespace Coffee.BehaviourTree.Decorator
{
    [Serializable]
    public class TreeConditionNode : TreeDecoratorNode
    {
        public TreeConditionNode(BehaviourTree tree) : base(tree)
        {
        }

        public override Result Execute()
        {
            throw new System.NotImplementedException();
        }

        public override void Reset()
        {
            throw new System.NotImplementedException();
        }
    }
}