using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BehaviourGraph.Services;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BehaviourGraph.Debugging
{
    [ShowOdinSerializedPropertiesInInspector]
    public class ServiceTester : MonoBehaviour
    {
        [Service]
        public IEnumerator DoThing(GameObject serviceExecutor) {
            Debug.Log(serviceExecutor.name);
            yield return null;
        }
        [Service]
        public IEnumerator DontDoThing(GameObject serviceExecutor) {
            Debug.Log(serviceExecutor.name);
            yield return null;
        }
        
    }
}