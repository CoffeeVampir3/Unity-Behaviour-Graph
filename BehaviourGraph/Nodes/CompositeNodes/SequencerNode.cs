using System;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Composite;
using Coffee.BehaviourTree.Ctx;
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
            sequencerNode = new TreeSequencerNode(null, null);
            thisTreeNode = sequencerNode;
        }

        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree, Context currentContext)
        {
            var node = new TreeSequencerNode(tree, currentContext);
            WalkCompositeNodeChildren(node, tree);
            return node;
        }
    }
}