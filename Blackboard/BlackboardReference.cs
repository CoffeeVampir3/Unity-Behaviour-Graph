using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    [CreateAssetMenu]
    public class BlackboardReference : SerializedScriptableObject
    {
        [NonSerialized, OdinSerialize, HideInInspector]
        public GameObject referencedObject;

        [NonSerialized, OdinSerialize]
        private ConditionalSelector editorTimeCondition = new ConditionalSelector();
        
        [NonSerialized]
        public BlackboardRuntimeCondition runtimeCondition = new BlackboardRuntimeCondition();

        private void OnValidate()
        {
            editorTimeCondition.Validate(this);
        }
    }
}