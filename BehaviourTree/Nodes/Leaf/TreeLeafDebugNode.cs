using System;
using UnityEngine;

namespace Coffee.BehaviourTree.Leaf
{
    [Serializable]
    public class TreeLeafDebugNode : TreeLeafNode
    {
        [SerializeField]
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

        public TreeLeafDebugNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}