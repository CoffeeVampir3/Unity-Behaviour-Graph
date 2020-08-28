using Coffee.BehaviourTree.Context;
using UnityEngine;

namespace Coffee.BehaviourTree.Leaf
{
    internal class TreeLeafDebugNode : TreeLeafNode
    {
        public string debugMessage;

        public override Result Execute(ref BehaviourContext context)
        {
            Debug.Log(debugMessage);
            return Result.Success;
        }

        public override void Reset()
        {
        }

        public TreeLeafDebugNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}