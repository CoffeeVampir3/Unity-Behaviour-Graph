using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.BehaviourTree.Leaf
{
    [Serializable]
    [ShowOdinSerializedPropertiesInInspector]
    public class TreeLeafDebugNode : TreeLeafNode
    {
        [NonSerialized, OdinSerialize]
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