using UnityEngine;

namespace Coffee.BehaviourTree.Leaf
{
    public class TreeLeafDebugNode : TreeLeafNode
    {
        public string debugMessage;

        public override Result Execute()
        {
            Debug.Log(debugMessage);
            return Result.Success;
        }

        public TreeLeafDebugNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}