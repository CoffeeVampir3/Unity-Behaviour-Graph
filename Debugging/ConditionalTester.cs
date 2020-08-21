using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace BehaviourGraph.Conditionals
{
    [ExecuteAlways]
    public class ConditionalTester : SerializedMonoBehaviour
    {
        public GameObject testObj;
        
        [Button]
        public void TestCachedConditions()
        {
            MethodInfo[] methods;
            FieldInfo[] fields;
            
            ConditionalCache.InitializeCache();
            
            var testThing = testObj.GetComponent<ConditionalTester>();
            if (ConditionalCache.TryGetCondition(testThing.GetType(), out fields))
            {
                foreach (var q in fields)
                {
                    Debug.Log(q.GetNiceName());
                    Debug.Log(q.GetValue(testThing));
                }
            }
            
            if (ConditionalCache.TryGetCondition(testThing.GetType(), out methods))
            {
                foreach (var q in methods)
                {
                    Debug.Log(q.GetNiceName());
                    
                    var k = CreateConditionFunction(q, testThing);
                    Debug.Log(k.Invoke());
                }
            }
        }
        
        public Func<bool> CreateConditionFunction(MethodInfo methodInfo, object target) {
            Func<Type[], Type> getType;
            
            getType = Expression.GetFuncType;
            var types = new[] {methodInfo.ReturnType};

            return (Func<bool>)Delegate.CreateDelegate(getType(types.ToArray()), target, methodInfo.Name);
        }
        
        public Delegate CreateDelegate(MethodInfo methodInfo, object target) {
            Func<Type[], Type> getType;
            var isAction = methodInfo.ReturnType.Equals((typeof(void)));
            var types = methodInfo.GetParameters().Select(p => p.ParameterType);

            if (isAction) {
                getType = Expression.GetActionType;
            }
            else {
                getType = Expression.GetFuncType;
                types = types.Concat(new[] { methodInfo.ReturnType });
            }

            if (methodInfo.IsStatic) {
                return Delegate.CreateDelegate(getType(types.ToArray()), methodInfo);
            }

            return Delegate.CreateDelegate(getType(types.ToArray()), target, methodInfo.Name);
        }

        [Condition] 
        public bool proceedIfTrue = false;

        [Condition]
        public bool mooses02 = true;
        [Condition]
        public bool mooses1 = true;
        [Condition]
        public bool mooses2 = true;
        [Condition]
        public bool mooses3 = true;
        [Condition]
        public bool mooses4 = true;

        [Condition]
        public bool DoThing()
        {
            return true;
        }
        
        [Condition]
        public bool OtherThing()
        {
            return false;
        }
    }
}