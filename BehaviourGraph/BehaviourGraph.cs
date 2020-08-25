using System;
using System.Collections.Generic;
using BehaviourGraph.Blackboard;
using Coffee.Behaviour.Nodes;
using Coffee.Behaviour.Nodes.Private;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;
using XNode;

namespace Coffee.Behaviour
{
    [CreateAssetMenu]
    [ShowOdinSerializedPropertiesInInspector]
    public class BehaviourGraph : NodeGraph, ISerializationCallbackReceiver
    {
        [SerializeField]
        protected GameObject pawn;
        [SerializeField]
        protected BaseNode root;
        [SerializeField]
        public BehaviourTree.BehaviourTree tree;
        [ShowInInspector, SerializeField]
        public LocalBlackboard localBlackboard;
        [SerializeField]
        public List<SharedBlackboard> blackboards = new List<SharedBlackboard>();

        public void Init()
        {
            if (tree != null)
                return;
            
            tree = CreateInstance<BehaviourTree.BehaviourTree>();
            root = AddNode<RootNode>();
            localBlackboard = CreateInstance<LocalBlackboard>();
            localBlackboard.name = "Local Blackboard";
            
            root.name = "Root Node";
            tree.name = "Behaviour Tree";
            
            tree.Init(root.thisTreeNode, ref localBlackboard, ref blackboards);
            
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(this));
            AssetDatabase.Refresh();
            AssetDatabase.AddObjectToAsset(root, this);
            AssetDatabase.AddObjectToAsset(localBlackboard, this);
            AssetDatabase.AddObjectToAsset(tree, this);
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(root));
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(tree));
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(localBlackboard));
            AssetDatabase.Refresh();
        }

        [SerializeField]
        [HideInInspector]
        private SerializationData serializationData;
        public void OnBeforeSerialize()
        {
            UnitySerializationUtility.SerializeUnityObject(this, ref serializationData);
        }

        public void OnAfterDeserialize()
        {
            UnitySerializationUtility.DeserializeUnityObject(this, ref serializationData);
        }
    }

}
