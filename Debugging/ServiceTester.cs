using System.Collections;
using BehaviourGraph.Attributes;
using Sirenix.OdinInspector;
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