using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    [CreateAssetMenu]
    public class BlackboardReference : SerializedScriptableObject
    {
        [NonSerialized, OdinSerialize] 
        private GameObject referencedObject;
        public GameObject ReferencedObject
        {
            set => referencedObject = value;
        }

        [Button]
        public void TestReference()
        {
            CacheRuntimeValues();
            Debug.Log(Evaluate());
        }

        [NonSerialized, OdinSerialize]
        private ConditionalSelector editorTimeCondition = new ConditionalSelector();
        
        [NonSerialized]
        public BlackboardRuntimeCondition runtimeCondition = new BlackboardRuntimeCondition();

        private void OnValidate()
        {
            editorTimeCondition.Validate(this);
        }

        public bool Evaluate()
        {
            return referencedObject != null && runtimeCondition.EvaluateAs(referencedObject);
        }

        public void CacheRuntimeValues()
        {
            runtimeCondition.RuntimeCacheSetup(editorTimeCondition);   
        }
    }
}