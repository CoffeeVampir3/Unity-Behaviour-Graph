using System;
using System.Linq.Expressions;
using System.Reflection;
using BehaviourGraph.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BehaviourGraph.Debugging
{
    public class ServiceTester : MonoBehaviour
    {
        [Button]
        public void TestServices()
        {
            MethodInfo[] methods;
            if (ServiceCache.TryGetServicesFor(GetType(), out methods))
            {
                foreach (var m in methods)
                {
                    Debug.Log(m.Name);

                    var k = CreateServiceAction(m, this);
                    k.Invoke(gameObject);
                }
            }
        }
        
        static Type[] gameObjectArgumentType = { typeof(GameObject) };
        private Action<GameObject> CreateServiceAction(MethodInfo methodInfo, object target)
        {
            Func<Type[], Type> getType = Expression.GetActionType;
            return (Action<GameObject>)Action.CreateDelegate(getType(gameObjectArgumentType), 
                target, methodInfo.Name);
        }
        
        [Service]
        public void DoThing(GameObject serviceExecutor) {
            Debug.Log(serviceExecutor.name);
        }
        [Service]
        public void DontDoThing(GameObject serviceExecutor) {
            Debug.Log(serviceExecutor.name);
        }
        
    }
}