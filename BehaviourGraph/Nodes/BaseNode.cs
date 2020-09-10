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
    internal abstract class BaseNode : Node, ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector]
        internal TreeBaseNode thisTreeNode;
        [SerializeField, HideInInspector]
        protected BehaviourGraph parentGraph;

        #region Coffee Nodes Impl
        
        public abstract TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree);
        protected abstract void OnCreation();
        
        #endregion

        #region Xnode Specific
        
        [SerializeField, HideInInspector]
        private bool initialized = false;
        protected override void Init()
        {
            if (initialized)
                return;
            
            parentGraph = graph as BehaviourGraph;
            UnityEngine.Debug.Assert(parentGraph != null, nameof(parentGraph) + " != null");
            
            initialized = true;
            OnCreation();
        }
        
        public override object GetValue(NodePort port)
        {
            return thisTreeNode;
        }
        
        #endregion

        #region ISerializationCallbackReceiver Impl
        
        [SerializeField, HideInInspector]
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