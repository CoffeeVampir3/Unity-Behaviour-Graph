using Coffee.BehaviourTree.Ctx;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeRepeaterNode : TreeDecoratorNode
    {
        public override Result Execute()
        {
            child.Execute();
            return Result.Running;
        }

        public override void Reset()
        {
            Debug.Assert(child != null);
            child.Reset();
        }

        public TreeRepeaterNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}