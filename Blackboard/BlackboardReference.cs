using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    [CreateAssetMenu]
    public class BlackboardReference : SerializedScriptableObject, IBlackboardReference
    {
        [NonSerialized, OdinSerialize] 
        protected GameObject referencedObject;

        [Button]
        public void TestReference()
        {
            CacheRuntimeValues();
            Debug.Log(Evaluate());
        }

        [NonSerialized, OdinSerialize]
        protected ConditionalSelector editorTimeCondition = new ConditionalSelector();
        
        [NonSerialized]
        protected BlackboardRuntimeCondition runtimeCondition = new BlackboardRuntimeCondition();

        protected void OnValidate()
        {
            editorTimeCondition.Validate(this);
        }

        public bool Evaluate()
        {
            return referencedObject != null && runtimeCondition.EvaluateAs(referencedObject);
        }

        public GameObject GetReference()
        {
            return referencedObject;
        }

        public void SetReference(GameObject go)
        {
            referencedObject = go;
        }

        public void CacheRuntimeValues()
        {
            runtimeCondition.RuntimeCacheSetup(editorTimeCondition);   
        }
    }
}