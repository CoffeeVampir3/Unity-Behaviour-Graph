using System;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Ctx;
using Coffee.BehaviourTree.Decorator;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.DecoratorNodes
{
    [Serializable]
    internal class RepeaterNode : DecoratorNode
    {
        [NonSerialized, OdinSerialize]
        [HideInInspector]
        protected TreeRepeaterNode repeaterNode;
        
        protected override void OnCreation()
        {
            repeaterNode = new TreeRepeaterNode(null, null);
            thisTreeNode = repeaterNode;
        }

        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree, Context currentContext)
        {
            var node = new TreeRepeaterNode(tree, currentContext);
            return WalkDecoratorNode(tree, node);
        }
    }
}