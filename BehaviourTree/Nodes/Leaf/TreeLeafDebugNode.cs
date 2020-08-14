using System;
using UnityEngine;

namespace Coffee.BehaviourTree.Leaf
{
    [Serializable]
    public class TreeLeafDebugNode : TreeLeafNode
    {
        public string debugMessage = "Default Debug Leaf Messages";

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