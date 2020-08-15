using BehaviourGraph.Blackboard;
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
        protected GameObject pawn;
        [SerializeField]
        protected BaseNode root;
        [SerializeField]
        public BehaviourTree.BehaviourTree tree;
        [SerializeField] 
        public IBlackboard blackboard;
        
        public void Init()
        {
            if (tree != null)
                return;
            
            tree = CreateInstance<BehaviourTree.BehaviourTree>();
            root = AddNode<RootNode>();
            root.name = "Root Node";
            tree.name = "Behaviour Tree";

            var bb = CreateInstance<Blackboard>();
            bb.name = "Blackboard";
            blackboard = bb;

            tree.Init(root.thisTreeNode, bb);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(this));
            AssetDatabase.Refresh();
            AssetDatabase.AddObjectToAsset(root, this);
            AssetDatabase.AddObjectToAsset(tree, this);
            AssetDatabase.AddObjectToAsset(bb, this);
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(root));
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(tree));
            AssetDatabase.Refresh();
        }
    }

}
