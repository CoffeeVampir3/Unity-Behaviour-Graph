using System;
using Coffee.Behaviour.Nodes;
using Coffee.Behaviour.Nodes.Private;
using UnityEditor;
using UnityEngine;
using XNode;

namespace Coffee.Behaviour
{
    [CreateAssetMenu]
    public class BehaviourGraph : NodeGraph
    {
        [SerializeField]
        private GameObject pawn;
        private BaseNode root;
        internal BehaviourTree.BehaviourTree tree;

        private bool isInitialized = false;
        public void Init()
        {
            if (!isInitialized)
            {
                tree = new BehaviourTree.BehaviourTree();
                root = AddNode<RootNode>();
                root.name = "Root Node";
                AssetDatabase.AddObjectToAsset(root, this);
                isInitialized = true;
            }
        }
    }

}
