using System;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Decorator;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.Private
{
    [Serializable]
    public class RootNode : RootDecoratorNode
    {
        [NonSerialized, OdinSerialize]
        [HideInInspector]
        protected TreeRootNode rootNode;
        
        protected override void OnCreation()
        {
            rootNode = new TreeRootNode(null);
            thisTreeNode = rootNode;
        }

        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree)
        {
            var treeRoot = rootNode;
            treeRoot.parentTree = tree;
            var p = GetOutputPort("childNode");

            BaseNode b = p.Connection.node as BaseNode;
            if (b == null)
            {
                Debug.LogError("Behaviour graph: Root was not connected to a child.", this);
                throw new NullReferenceException("Behaviour graph could not build into a valid tree due to null children.");
            }

            treeRoot.child = b.WalkGraphToCreateTree(tree);
            return treeRoot;
        }
    }
}