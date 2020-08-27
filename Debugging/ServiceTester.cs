﻿using System;
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
        [Button]
        public void TestServices()
        {
            MethodInfo[] methods;
            if (ServiceCache.TryGetServicesFor(GetType(), out methods))
            {
                foreach (var m in methods)
                {
                    Debug.Log(m.Name);

                    var k = ServiceCreator.CreateServiceFunction(m, this);
                    k.Invoke(gameObject);
                }
            }
        }

        [OdinSerialize]
        [ValueDropdown("GetServices", NumberOfItemsBeforeEnablingSearch = 2)]
        public MethodInfo targetMethod;
        public ValueDropdownList<MethodInfo> GetServices => ServiceCache.GetListOfServices();

        [Service]
        public IEnumerator DoThing(GameObject serviceExecutor) {
            Debug.Log(serviceExecutor.name);
            return null;
        }
        [Service]
        public IEnumerator DontDoThing(GameObject serviceExecutor) {
            Debug.Log(serviceExecutor.name);
            return null;
        }
        
    }
}