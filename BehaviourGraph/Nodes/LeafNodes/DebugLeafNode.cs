using Coffee.BehaviourTree.Leaf;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.LeafNodes
{
    public class DebugLeafNode : LeafNode
    {
        [SerializeField]
        public string debugMessage;
        
        protected override void OnCreation()
        {
            thisTreeNode = new TreeLeafDebugNode(parentTree);
            parentTree.nodes.Add(thisTreeNode);
        }
    }
}