using System;
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

        [OdinSerialize]
        [ValueDropdown("GetServices", NumberOfItemsBeforeEnablingSearch = 2)]
        public MethodInfo targetMethod;

        public ValueDropdownList<MethodInfo> GetServices()
        {
            MethodInfo[] methods;
            ValueDropdownList<MethodInfo> servicesList = new ValueDropdownList<MethodInfo>();
            if (ServiceCache.TryGetServicesFor(GetType(), out methods))
            {
                foreach (var m in methods)
                {
                    servicesList.Add(m.DeclaringType.Name + "/" + m.Name, m);
                }
            }
            return servicesList;
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