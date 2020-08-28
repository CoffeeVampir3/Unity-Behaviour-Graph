using System;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Composite;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.CompositeNodes
{
    [Serializable]
    internal class SequencerNode : CompositeNode
    {
        [NonSerialized, OdinSerialize]
        [HideInInspector]
        protected TreeSequencerNode sequencerNode;
        
        protected override void OnCreation()
        {
            sequencerNode = new TreeSequencerNode(null);
            thisTreeNode = sequencerNode;
        }

        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree)
        {
            var node = new TreeSequencerNode(tree);
            WalkCompositeNodeChildren(node, tree);
            return node;
        }
    }
}