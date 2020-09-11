using Coffee.BehaviourTree.Ctx;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeRootNode : TreeDecoratorNode
    {
        public override Result Execute()
        {
            return child.Execute();
        }

        public override void Reset()
        {
            Debug.Assert(child != null);
            child.Reset();
        }

        public TreeRootNode(BehaviourTree tree) : base(tree)
        {
            contextBlock = new ContextBlock(null, this);
        }
    }
}