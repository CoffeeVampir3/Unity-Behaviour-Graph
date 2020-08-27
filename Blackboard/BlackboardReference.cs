using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    [CreateAssetMenu]
    public class BlackboardReference : SerializedScriptableObject
    {
        [NonSerialized] 
        private GameObject referencedObject;
        public GameObject ReferencedObject
        {
            set
            {
                referencedObject = value;
            }
        }

        public bool Evaluate()
        {
            return referencedObject != null && runtimeCondition.EvaluateAs(referencedObject);
        }

        [SerializeField, HideInInspector]
        internal Blackboard parentBlackboard;
        [Button(ButtonSizes.Gigantic)]
        private void DeleteReference()
        {
            parentBlackboard.RemoveReference(this);
            DestroyImmediate(this, true);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [NonSerialized, OdinSerialize]
        internal ConditionalSelector editorTimeCondition = new ConditionalSelector();
        
        public bool referencesBlackboardOwner = true;
        
        [NonSerialized] 
        private BlackboardRuntimeCondition runtimeCondition = new BlackboardRuntimeCondition();
        
        internal void CacheRuntimeValues()
        {
            runtimeCondition.RuntimeCacheSetup(editorTimeCondition);   
        }
    }
}