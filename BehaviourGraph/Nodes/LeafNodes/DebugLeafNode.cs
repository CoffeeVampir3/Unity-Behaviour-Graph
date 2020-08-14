using Coffee.BehaviourTree.Leaf;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.LeafNodes
{
    public class DebugLeafNode : LeafNode
    {
        [SerializeField]
        public string debugMessage;
        
        protected override void Init()
        {
            base.Init();
            thisTreeNode = new TreeLeafDebugNode(parentTree);
            parentTree.nodes.Add(thisTreeNode);
        }
    }
}