using System;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Ctx;
using Coffee.BehaviourTree.Decorator;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.DecoratorNodes
{
    [Serializable]
    internal class InvertNode : DecoratorNode
    {
        [NonSerialized, OdinSerialize]
        [HideInInspector]
        protected TreeInvertNode invertNode;
        
        protected override void OnCreation()
        {
            invertNode = new TreeInvertNode(null, null);
            thisTreeNode = invertNode;
        }
        
        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree, Context currentContext)
        {
            var node = new TreeInvertNode(tree, currentContext);
            return WalkDecoratorNode(tree, node);
        }
    }
}