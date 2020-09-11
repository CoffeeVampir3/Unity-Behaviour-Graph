using Coffee.BehaviourTree.Context;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeRootNode : TreeDecoratorNode
    {
        public override Result Execute(ref BehaviourContext context)
        {
            return child.Execute(ref context);
        }

        public override void Reset()
        {
            Debug.Assert(child != null);
            child.Reset();
        }

        public TreeRootNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}