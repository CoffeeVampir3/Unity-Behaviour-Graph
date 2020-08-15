using System;
using Coffee.BehaviourTree.Leaf;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.LeafNodes
{
    [Serializable]
    public class DebugLeafNode : LeafNode
    {
        [SerializeField]
        protected TreeLeafDebugNode debugNode;
        
        protected override void OnCreation()
        {
            debugNode = new TreeLeafDebugNode(parentTree);
            thisTreeNode = debugNode;
        }
    }
}