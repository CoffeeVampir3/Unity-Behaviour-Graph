using System;
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
            MemberInfo[] k = TypeCache.GetFieldsWithAttribute<Condition>().ToArray();

            fieldStore = AttributeStore.Create<FieldAttributeStore>(ref k);
            
            MemberInfo[] x = TypeCache.GetMethodsWithAttribute<Condition>().ToArray();

            methodStore = AttributeStore.Create<MethodAttributeStore>(ref x);
        }

        [Button]
        public void DebugFieldCereal()
        {
            var elk = u.GetComponent<ConditionalTester>();
            var q = fieldStore.RetreiveAsField();
            foreach (var f in q)
            {
                if(f.DeclaringType == u.GetType())
                    Debug.Log(f.GetValue(u));
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
        
        private Func<bool> CreateConditionFunction(MethodInfo methodInfo, object target) {
            Func<Type[], Type> getType = Expression.GetFuncType;
            var types = new[] {methodInfo.ReturnType};

            return (Func<bool>)Delegate.CreateDelegate(getType(types.ToArray()), target, methodInfo.Name);
        }
        
    }
}