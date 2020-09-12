using Coffee.BehaviourTree.Ctx;
using UnityEngine;

namespace Coffee.BehaviourTree.Leaf
{
    internal class TreeLeafDebugNode : TreeLeafNode
    {
        public string debugMessage;

        public override Result Execute()
        {
            Debug.Log(debugMessage);
            return Result.Success;
        }

        public override void Reset()
        {
            //Empty
        }

        public TreeLeafDebugNode(BehaviourTree tree, Context parentCtx) : 
            base(tree, parentCtx)
        {
        }
    }
}