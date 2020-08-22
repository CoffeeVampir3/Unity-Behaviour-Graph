using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    [CreateAssetMenu]
    public class Blackboard : SerializedScriptableObject
    {
        [NonSerialized, OdinSerialize] 
        public List<BlackboardReference> references;
        
        public void RuntimeInitialize()
        {
        }
    }
}