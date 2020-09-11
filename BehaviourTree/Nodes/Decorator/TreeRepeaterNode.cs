using Coffee.BehaviourTree.Context;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeRepeaterNode : TreeDecoratorNode
    {
        public override Result Execute(ref BehaviourContext context)
        {
            child.Execute(ref context);
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