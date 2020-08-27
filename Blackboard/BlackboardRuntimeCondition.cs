using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BehaviourGraph.Conditionals;
using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    [Serializable]
    internal class BlackboardRuntimeCondition
    {
        private bool isMethod = false;
        private Type conditionType;
        private MethodInfo method;
        private FieldInfo field;
        private Func<bool> cachedConditionFunction;

        private int cachedObjectHash = -1;
        private Component cachedConditionComponent;
        public bool EvaluateAs(GameObject obj)
        {
            if (obj == null)
                return false;
            
            if (cachedObjectHash != obj.GetHashCode())
            {
                cachedObjectHash = obj.GetHashCode();
                SetupCachedVersionFor(obj);
            }
            
            if (isMethod)
            {
                return cachedConditionFunction.Invoke();
            }

            return (bool)field.GetValue(cachedConditionComponent);
        }

        public void SetupCachedVersionFor(GameObject obj)
        {
            if (!obj.TryGetComponent(conditionType, out cachedConditionComponent))
            {
                throw new Exception("Unable to find condition " + conditionType.Name + " in " + obj.name);
            }
            if (isMethod)
            {
                cachedConditionFunction = CreateConditionFunction(method, cachedConditionComponent);
            }
        }
        
        public Func<bool> CreateConditionFunction(MethodInfo methodInfo, object target) {
            Func<Type[], Type> getType;
            
            getType = Expression.GetFuncType;
            var types = new[] {methodInfo.ReturnType};

            return (Func<bool>)Delegate.CreateDelegate(getType(types.ToArray()), target, methodInfo.Name);
        }
        
        public void RuntimeCacheSetup(ConditionalSelector selector)
        {
            isMethod = selector.isMethod;

            var m = selector.MemberSelector;
            conditionType = m.DeclaringType;
            if (isMethod)
            {
                method = m as MethodInfo;
            }
            else
            {
                field = m as FieldInfo;
            }
        }
    }
}