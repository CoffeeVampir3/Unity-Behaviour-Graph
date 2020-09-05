using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BehaviourGraph.Conditionals;
using BehaviourGraph.Services;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Expression = System.Linq.Expressions.Expression;

namespace BehaviourGraph.Debugging
{
    public class CacheDebugger : MonoBehaviour
    {
        [SerializeField] 
        private GameObject u = null;
        
        [SerializeField]
        public FieldAttributeStore fieldStore;

        [SerializeField] 
        public MethodAttributeStore methodStore;

        [Button]
        public void MakeCache()
        {
            var conditionFields = TypeCache.GetFieldsWithAttribute<Condition>().ToArray();
            var conditionMethods = TypeCache.GetMethodsWithAttribute<Condition>().ToArray();
            var serviceMethods = TypeCache.GetMethodsWithAttribute<Service>().ToArray();

            AttributeCacheRetainer.CacheOrGetCachedAttributeData<FieldInfo, Condition>(
                conditionFields,
                out var conditionalFieldsDict);

            AttributeCacheRetainer.CacheOrGetCachedAttributeData<MethodInfo, Condition>(
                conditionMethods,
                out var conditionalMethodsDict);

            AttributeCacheRetainer.CacheOrGetCachedAttributeData<MethodInfo, Service>(
                serviceMethods,
                out var serviceMethodsDict);
        }

        [Button]
        public void DebugDirectory()
        {
            var uPha = AttributeCacheRetainer.GetStoreContainer();

            var l = uPha.GetFields();
            var k = uPha.GetMethods();

            foreach (var x in l)
            {
                var xF = x.RetrieveAsField();
                foreach (var z in xF)
                {
                    Debug.Log(z.Name);
                }
            }
            
            foreach (var x in k)
            {
                var xF = x.RetrieveAsMethod();
                foreach (var z in xF)
                {
                    Debug.Log(z.Name);
                }
            }
        }

        [Button]
        public void DebugRuntime()
        {
            var conditionFields = TypeCache.GetFieldsWithAttribute<Condition>().ToArray();
            var conditionMethods = TypeCache.GetMethodsWithAttribute<Condition>().ToArray();
            var serviceMethods = TypeCache.GetMethodsWithAttribute<Service>().ToArray();

            var a = AttributeCacheRetainer.RuntimeGetFromCache<FieldInfo, Condition>(
                conditionFields,
                out var conditionalFieldsDict);

            var b = AttributeCacheRetainer.RuntimeGetFromCache<MethodInfo, Condition>(
                conditionMethods,
                out var conditionalMethodsDict);

            var c = AttributeCacheRetainer.RuntimeGetFromCache<MethodInfo, Service>(
                serviceMethods,
                out var serviceMethodsDict);

            foreach (var l in a)
            {
                Debug.Log(l.Name);
            }
            foreach (var l in b)
            {
                Debug.Log(l.Name);
            }
            foreach (var l in c)
            {
                Debug.Log(l.Name);
            }
        }

        [Button]
        public void LoadCachedAttributeData()
        {
            if (AttributeCacheRetainer.fieldStores != null)
            {
                foreach (var m in AttributeCacheRetainer.fieldStores)
                {
                    if (m.cachedAttributeType == typeof(Condition))
                    {
                        var k = m.RetrieveAsField();
                        foreach (var w in k)
                        {
                            Debug.Log(w.Name);
                        }
                    }
                }
            }
            
            if (AttributeCacheRetainer.methodStores != null)
            {
                foreach (var m in AttributeCacheRetainer.methodStores)
                {
                    if (m.cachedAttributeType == typeof(Condition))
                    {
                        var k = m.RetrieveAsMethod();
                        foreach (var w in k)
                        {
                            Debug.Log(w.Name);
                        }
                    }
                }
            }
        }

        private class A
        {
        }

        private class B : A
        {
        }

        [Button]
        public void TestK()
        {
            var tA = typeof(A);
            var tB = typeof(B);
            bool aAssignB = tA.IsAssignableFrom(tB); // (A) = b
            bool aAssignA = tA.IsAssignableFrom(tA);
            bool bAssignA = tB.IsAssignableFrom(tA); // (B) = a

            Debug.Log(aAssignB);
            Debug.Log(aAssignA);
            Debug.Log(bAssignA);
        }

        private Func<bool> CreateConditionFunction(MethodInfo methodInfo, object target) {
            Func<Type[], Type> getType = Expression.GetFuncType;
            var types = new[] {methodInfo.ReturnType};

            return (Func<bool>)Delegate.CreateDelegate(getType(types.ToArray()), target, methodInfo.Name);
        }
        
    }
}