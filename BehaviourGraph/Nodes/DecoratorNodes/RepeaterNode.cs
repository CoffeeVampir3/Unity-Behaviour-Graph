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
            var p = GetOutputPort("childNode");

            BaseNode b = p.Connection.node as BaseNode;
            if (b == null)
            {
                Debug.LogError("Behaviour graph node: " + this.name + " was not connected to a child.", this);
                throw new NullReferenceException("Behaviour graph could not build into a valid tree due to null children.");
            }

            node.child = b.WalkGraphToCreateTree(tree);
            return node;
        }
    }
}