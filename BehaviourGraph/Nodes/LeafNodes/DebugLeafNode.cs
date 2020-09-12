using System;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Ctx;
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
            debugNode = new TreeLeafDebugNode(null, null);
            thisTreeNode = debugNode;
        }
        
        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree, Context currentContext)
        {
            var node = new TreeLeafDebugNode(tree, currentContext) 
                {debugMessage = debugNote};
            return node;
        }
    }
}