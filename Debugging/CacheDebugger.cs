using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BehaviourGraph.Conditionals;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace BehaviourGraph.Debugging
{
    public class CacheDebugger : MonoBehaviour
    {
        [SerializeField] 
        private GameObject u;
        
        [SerializeField]
        public FieldAttributeStore fieldStore;

        [SerializeField] 
        public MethodAttributeStore methodStore;

        [Button]
        public void MakeCache()
        {
            AttributeCacheRetainer.SetupOrSaveCache();
            
            FieldInfo[] k = TypeCache.GetFieldsWithAttribute<Condition>().ToArray();
            MethodInfo[] x = TypeCache.GetMethodsWithAttribute<Condition>().ToArray();
            Dictionary<(Type, Type), MethodInfo[]> conditionalMethods = new Dictionary<(Type, Type), MethodInfo[]>();
            Dictionary<(Type, Type), FieldInfo[]> conditionalFields = new Dictionary<(Type, Type), FieldInfo[]>();

            Debug.Log("Field Len: " + k.Length);
            Debug.Log("Method Len: " + x.Length);
            AttributeCacheRetainer.EditorTimeCache<FieldInfo, Condition>(
                ref conditionalFields, ref k, null);
            
            AttributeCacheRetainer.EditorTimeCache<MethodInfo, Condition>(
                ref conditionalMethods, ref x, null);
        }

        [Button]
        public void DebugFieldCereal()
        {
            var elk = u.GetComponent<ConditionalTester>();
            var q = fieldStore.RetreiveAsField();
            foreach (var f in q)
            {
                if(f.DeclaringType == elk.GetType())
                    Debug.Log(f.GetValue(elk));
            }
        }
        
        [Button]
        public void DebugMethodCereal()
        {
            var elk = u.GetComponent<ConditionalTester>();
            var q = methodStore.RetreiveAsMethod();
            Debug.Log(q.Length);
            foreach (var f in q)
            {
                var l = CreateConditionFunction(f, elk);
                Debug.Log(l.Invoke());
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