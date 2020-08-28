using System.Collections.Generic;
using BehaviourGraph.Blackboard;
using Coffee.Behaviour.Nodes;
using Coffee.Behaviour.Nodes.Private;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Decorator;
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
        internal BaseNode root;
        [HideInInspector, SerializeField]
        public Blackboard localBlackboard;
        [SerializeField] 
        public List<Blackboard> blackboards = new List<Blackboard>();

        public void EditorTimeInitialization()
        {
            if (localBlackboard != null)
                return;
            
            root = AddNode<RootNode>();
            localBlackboard = CreateInstance<Blackboard>();
            localBlackboard.name = "Local Blackboard";
            root.name = "Root Node";
            
            blackboards.Add(localBlackboard);

            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(this));
            AssetDatabase.Refresh();
            AssetDatabase.AddObjectToAsset(root, this);
            AssetDatabase.AddObjectToAsset(localBlackboard, this);
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(root));
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(localBlackboard));
            AssetDatabase.Refresh();
        }

        public BehaviourTree.BehaviourTree GenerateBehaviourTree(GameObject executingOn)
        {
            Debug.Log("Creating new tree.");
            var cloneTree = new BehaviourTree.BehaviourTree();
            TreeBaseNode treeRoot = root.WalkGraphToCreateTree(cloneTree);
            cloneTree.Init(treeRoot, ref blackboards);
            cloneTree.RuntimeSetup(executingOn);

            return cloneTree;
        }

        public ValueDropdownList<BlackboardReference> GetAllBlackboardReferences()
        {
            ValueDropdownList<BlackboardReference> refs = new ValueDropdownList<BlackboardReference>();
            
            foreach (var bb in blackboards)
            {
                refs.AddRange(bb?.GetBlackboardReferences());
            }

            return refs;
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
