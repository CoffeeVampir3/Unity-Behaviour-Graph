﻿using Coffee.Behaviour.Nodes;
using Coffee.Behaviour.Nodes.Private;
using Coffee.BehaviourTree;
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
        internal BaseNode root = null;
        
        public BehaviourTree.BehaviourTree GenerateBehaviourTree(GameObject executingOn)
        {
            var cloneTree = new BehaviourTree.BehaviourTree(executingOn);
            TreeBaseNode treeRoot = root.WalkGraphToCreateTree(cloneTree, null);
            cloneTree.RuntimeSetup(treeRoot, executingOn);
            treeRoot.Reset();
            
            return cloneTree;
        }
        
#if UNITY_EDITOR
        #region Editor Initialization
        /// <summary>
        /// Bootstraps our root when the graph is created.
        /// </summary>
        public void EditorTimeInitialization()
        {
            if (root != null)
                return;
            
            root = AddNode<RootNode>();
            root.name = "Root Node";

            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(this));
            AssetDatabase.Refresh();
            AssetDatabase.AddObjectToAsset(root, this);
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(root));
            AssetDatabase.Refresh();
        }

        #endregion
#endif

        //ODIN dependency
        #region ISerializationCallbackReceiver Impl
        
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
        
        #endregion
    }

}
