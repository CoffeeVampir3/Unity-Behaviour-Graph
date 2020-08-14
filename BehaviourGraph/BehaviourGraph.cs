using System;
using Coffee.Behaviour.Nodes;
using Coffee.Behaviour.Nodes.Private;
using Coffee.BehaviourTree;
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
        [SerializeField]
        private BaseNode root;
        [SerializeField]
        public BehaviourTree.BehaviourTree tree;
        
        public void Init()
        {
            if (tree != null)
                return;
            
            tree = CreateInstance<BehaviourTree.BehaviourTree>();
            tree.Init();
            root = AddNode<RootNode>();
            root.name = "Root Node";
            tree.name = "Behaviour Tree";

            tree.root = root.thisTreeNode;
            
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(this));
            AssetDatabase.Refresh();
            AssetDatabase.AddObjectToAsset(root, this);
            AssetDatabase.AddObjectToAsset(tree, this);
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(root));
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(tree));
            AssetDatabase.Refresh();
        }
    }

}
