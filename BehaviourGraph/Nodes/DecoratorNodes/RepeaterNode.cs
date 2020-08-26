using System;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Decorator;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.DecoratorNodes
{
    [Serializable]
    public class RepeaterNode : DecoratorNode
    {
        [NonSerialized, OdinSerialize]
        [HideInInspector]
        protected TreeRepeaterNode repeaterNode;
        
        protected override void OnCreation()
        {
            repeaterNode = new TreeRepeaterNode(null);
            thisTreeNode = repeaterNode;
        }

        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree)
        {
            var node = repeaterNode;
            node.parentTree = tree;
            return WalkDecoratorNode(tree, node);
        }
    }
}