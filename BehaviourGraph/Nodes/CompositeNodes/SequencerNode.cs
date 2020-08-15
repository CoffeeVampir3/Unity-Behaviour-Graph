using System;
using Coffee.BehaviourTree.Composite;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.CompositeNodes
{
    [Serializable]
    public class SequencerNode : CompositeNode
    {
        [SerializeField] 
        [HideInInspector]
        protected TreeSequencerNode sequencerNode;
        
        protected override void OnCreation()
        {
            sequencerNode = new TreeSequencerNode(parentTree);
            thisTreeNode = sequencerNode;
        }
    }
}