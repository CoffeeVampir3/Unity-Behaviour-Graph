using System;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Leaf;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.LeafNodes
{
    [Serializable]
    internal class DebugLeafNode : LeafNode
    {
        [SerializeField] 
        public string debugNote = "";
        [NonSerialized, OdinSerialize, HideInInspector]
        protected TreeLeafDebugNode debugNode;
        
        protected override void OnCreation()
        {
            debugNode = new TreeLeafDebugNode(null);
            thisTreeNode = debugNode;
        }
        
        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree)
        {
            var node = new TreeLeafDebugNode(tree) {debugMessage = debugNote};
            return node;
        }
    }
}