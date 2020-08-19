using System;
using Coffee.BehaviourTree;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using XNode;

namespace Coffee.Behaviour.Nodes
{
    [Serializable]
    [ShowOdinSerializedPropertiesInInspector]
    public abstract class BaseNode : Node, ISerializationCallbackReceiver
    {
        [SerializeField]
        [HideInInspector]
        public TreeBaseNode thisTreeNode;
        [SerializeField]
        [HideInInspector]
        protected BehaviourGraph parentGraph;
        [SerializeField]
        [HideInInspector]
        protected BehaviourTree.BehaviourTree parentTree;

        protected abstract void OnCreation();

        [SerializeField]
        [HideInInspector]
        private bool initialized = false;
        protected override void Init()
        {
            if (initialized)
                return;
            
            base.Init();
            parentGraph = graph as BehaviourGraph;
            Debug.Assert(parentGraph != null, nameof(parentGraph) + " != null");
            
            parentTree = parentGraph.tree;
            initialized = true;

            OnCreation();
        }
        
        public override object GetValue(NodePort port)
        {
            return thisTreeNode;
        }

        [OdinSerialize]
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