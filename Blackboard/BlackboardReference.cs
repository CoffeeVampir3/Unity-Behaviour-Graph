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

        [SerializeField, HideInInspector]
        internal SharedBlackboard parentBlackboard;
        [Button(ButtonSizes.Gigantic)]
        public void DeleteReference()
        {
            parentBlackboard.RemoveReference(this);
            DestroyImmediate(this, true);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public bool TryGetDisplayName(out string outputString)
        {
            if (!editorTimeCondition.TryGetConditionDisplayValue(out outputString))
                return false;
            outputString = outputString.Insert(0, name + ", ");
            return true;
        }

        [NonSerialized, OdinSerialize]
        internal ConditionalSelector editorTimeCondition = new ConditionalSelector();
        
        [NonSerialized] 
        private BlackboardRuntimeCondition runtimeCondition = new BlackboardRuntimeCondition();

        protected void OnValidate()
        {
            editorTimeCondition.Validate(this);
        }

        public bool Evaluate()
        {
            return referencedObject != null && runtimeCondition.EvaluateAs(referencedObject);
        }

        internal void CacheRuntimeValues()
        {
            runtimeCondition.RuntimeCacheSetup(editorTimeCondition);   
        }
    }
}